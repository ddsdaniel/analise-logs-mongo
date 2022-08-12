using AnaliseGrafana.Configurations;
using AnaliseGrafana.Factories;
using AnaliseGrafana.Models;
using AnaliseGrafana.Models.TorusPDV;
using AnaliseGrafana.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnaliseGrafana.Forms
{
    public partial class FrmPrincipal : Form
    {
        private IEnumerable<Log> _logs;

        private enum ColunasResultados
        {
            Inicial = 0,
            Final,
            Ocorrencias,
            Percentual,
            Acumulado
        }

        private enum ColunasRequests
        {
            Metodo = 0,
            Request,
            Ocorrencias,
            TempoMedio,
            TempoMinimo,
            TempoMaximo
        }

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            MongoConfig.Configurar();

            FormatarLvwResultados();
            FormatarLvwRequests();
            RecuperarFiltros();
        }

        private void RecuperarFiltros()
        {
            var serializacaoService = new SerializacaoService();

            var filtros = serializacaoService.Desserializar<Filtro>("filtros.xml");

            if (filtros == null)
                return;

            dtpInicial.Value = filtros.DataInicial;
            dtpFinal.Value = filtros.DataFinal;
            txtCriterio.Text = filtros.Criterio;
            txtMetodo.Text = filtros.Metodo;

        }

        private static string ObterLinhaPlanilha(LogTorusPDV log)
        {
            var requestPath = log.Properties.RequestPath;
            var duracao = (log.Properties.RequestTime / 1000).ToString("#,##0.0");
            var tipoComando = LerCampoJson(log.Properties.ResponseBody, "tipoComando");
            var buffer = LerCampoJson(log.Properties.ResponseBody, "buffer").Replace("\n", "{enter}").Replace("\r", "");

            return $"{requestPath}\t{duracao}\t{tipoComando}\t{buffer}";
        }

        private static string LerCampoJson(string json, string campo)
        {
            if (!String.IsNullOrEmpty(json))
            {
                var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (dic.ContainsKey(campo))
                    return dic[campo];
            }
            return "";
        }

        private void FormatarLvwRequests()
        {
            AplicarFormatacaoBasicaListView(lvwRequests);
            lvwRequests.Columns.Add("Método", 70);
            lvwRequests.Columns.Add("Request", 400);
            lvwRequests.Columns.Add("Ocorrências", 100, HorizontalAlignment.Right);
            lvwRequests.Columns.Add("Tempo Médio", 100, HorizontalAlignment.Right);
            lvwRequests.Columns.Add("Menor Tempo", 100, HorizontalAlignment.Right);
            lvwRequests.Columns.Add("Maior Tempo", 100, HorizontalAlignment.Right);
        }

        private void FormatarLvwResultados()
        {
            AplicarFormatacaoBasicaListView(lvwResultados);

            lvwResultados.Columns.Add("Inicial", 100);
            lvwResultados.Columns.Add("Final", 100);
            lvwResultados.Columns.Add("Ocorrências");
            lvwResultados.Columns.Add("Percentual", 100, HorizontalAlignment.Right);
            lvwResultados.Columns.Add("Acumulado", 100, HorizontalAlignment.Right);
        }

        private void AplicarFormatacaoBasicaListView(ListView listView)
        {
            listView.Columns.Clear();
            listView.View = View.Details;
            listView.MultiSelect = false;
            listView.FullRowSelect = true;
        }

        private void btnAnalisar_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                SalvarFiltros();
                Analisar();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void SalvarFiltros()
        {
            var serializacaoService = new SerializacaoService();
            var filtros = ObterFiltros();
            serializacaoService.Serializar("filtros.xml", filtros);
        }

        private Filtro ObterFiltros()
        {
            return new Filtro
            {
                DataInicial = dtpInicial.Value,
                DataFinal = dtpFinal.Value,
                Criterio = txtCriterio.Text,
                Metodo = txtMetodo.Text                
            };
        }

        private void Analisar()
        {
            var filtro = ObterFiltros();
            var importacaoService = new ImportacaoMongoService(filtro);
            var analiseService = new AnaliseService();

            lvwResultados.Items.Clear();
            lvwRequests.Items.Clear();

            _logs = importacaoService.Importar();
            if (_logs.Any())
            {
                var resultados = analiseService.Analisar(_logs);

                foreach (var resultado in resultados)
                {
                    var item = new ListViewItem
                    {
                        Text = resultado.Intervalo.Inicial.ToString()
                    };
                    item.SubItems.Add(resultado.Intervalo.Final.ToString());
                    item.SubItems.Add(resultado.Ocorrencias.ToString("#,##0"));
                    item.SubItems.Add(resultado.Percentual.ToString("#0.0") + "%");
                    item.SubItems.Add(resultado.PercentualAcumulado.ToString("#0.0") + "%");

                    lvwResultados.Items.Add(item);
                }

                AnalisarRequests(_logs);
            }

        }

        private void AnalisarRequests(IEnumerable<Models.Log> logs)
        {
            var analisaRequestService = new AnaliseRequestsService();
            var analise = analisaRequestService.Analisar(logs);
            foreach (var resultado in analise)
            {
                var item = new ListViewItem
                {
                    Text = resultado.RequestMethod
                };
                item.SubItems.Add(resultado.RequestPath);
                item.SubItems.Add(resultado.Ocorrencias.ToString("#,##0"));
                item.SubItems.Add(resultado.TempoMedio.ToString("#,##0.0") + " seg");
                item.SubItems.Add(resultado.TempoMinimo.ToString("#,##0.0") + " seg");
                item.SubItems.Add(resultado.TempoMaximo.ToString("#,##0.0") + " seg");

                lvwRequests.Items.Add(item);
            }
        }

        private void lvwResultados_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwResultados.SelectedItems.Count == 0 || _logs == null)
                return;
            
            var tempoInicial = Convert.ToInt32(lvwResultados.SelectedItems[0].SubItems[(int)ColunasResultados.Inicial].Text) * 1000;
            var tempoFinal = Convert.ToInt32(lvwResultados.SelectedItems[0].SubItems[(int)ColunasResultados.Final].Text) * 1000;

            var formDetalhe = new FrmDetalhe
            {
                Icon = Icon,
                Logs = _logs.Where(l => l.DuracaoMilliSeconds >= tempoInicial && l.DuracaoMilliSeconds <= tempoFinal)
            };
            formDetalhe.ShowDialog();
        }

        private void btnDetalhar_Click(object sender, EventArgs e)
        {
            if (_logs == null)
                return;

            var formDetalhe = new FrmDetalhe
            {
                Icon = Icon,
                Logs = _logs
            };
            formDetalhe.ShowDialog();
        }
    }
}
