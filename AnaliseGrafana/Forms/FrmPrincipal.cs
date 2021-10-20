using AnaliseGrafana.Configurations;
using AnaliseGrafana.Factories;
using AnaliseGrafana.Models.TorusPDV;
using AnaliseGrafana.Services;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AnaliseGrafana.Forms
{
    public partial class FrmPrincipal : Form
    {
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

            dtpInicial.Value = Convert.ToDateTime("03/09/2021");
            dtpFinal.Value = Convert.ToDateTime("03/09/2021 23:59:59");

            //AnalisarTef();
        }

        private void AnalisarTef()
        {
            var collectionFactory = new CollectionFactory();
            var collection = collectionFactory.Criar<LogTorusPDV>("logs");

            var dataInicial = Convert.ToDateTime("03/09/2021 18:38");
            var dataFinal = Convert.ToDateTime("03/09/2021 18:40");

            var logsTorus = collection
                .AsQueryable()
                .Where(l => !l.Properties.RequestPath.EndsWith("Hub") &&
                            l.Timestamp >= dataInicial &&
                            l.Timestamp <= dataFinal &&
                            l.Properties.RequestPath.Contains("/Tef/")
                            )
                .OrderBy(l => l.Timestamp)
                .ToList();

            var array = logsTorus
                .Select(l => ObterLinhaPlanilha(l))
                .ToList();

            var logs = string.Join("\n", array);

            //var array = logsTorus
            //    .Select(l =>
            //    {

            //        var proximoComando = "";

            //        if (l.Properties != null && !String.IsNullOrEmpty(l.Properties.ResponseBody))
            //        {
            //            var dic = JsonConvert.DeserializeObject<Dictionary<string, string>>(l.Properties.RequestBody);
            //            if (dic.ContainsKey("proximoComando"))
            //                proximoComando = dic["proximoComando"];
            //        }
            //        return new
            //        {
            //            RequestPath = l.Properties.RequestPath,
            //            l.Properties.RequestTime,
            //            ProximoComando = proximoComando
            //        };
            //    })
            //    .GroupBy(l => new
            //    {
            //        l.RequestPath,
            //        l.ProximoComando
            //    })
            //    .Select(g => new
            //    {
            //        g.First().RequestPath,
            //        g.First().ProximoComando,
            //        Duracao = Math.Round(g.Sum(l => l.RequestTime) / 1000, 1, MidpointRounding.AwayFromZero)
            //    })
            //    .OrderByDescending(x => x.Duracao)
            //    .ToList();

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

        private void Analisar()
        {
            var importacaoService = new ImportacaoMongoService(dtpInicial.Value, dtpFinal.Value, txtCriterio.Text);
            var analiseService = new AnaliseService();

            lvwResultados.Items.Clear();
            lvwRequests.Items.Clear();

            var logs = importacaoService.Importar();
            if (logs.Any())
            {
                var resultados = analiseService.Analisar(logs);

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

                AnalisarRequests(logs);
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
    }
}
