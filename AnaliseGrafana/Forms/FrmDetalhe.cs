using AnaliseGrafana.Models;
using AnaliseGrafana.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AnaliseGrafana.Forms
{
    public partial class FrmDetalhe : Form
    {
        private enum Colunas
        {
            DataHora = 0,
            Duracao,
            Gap,
            Metodo,
            Request,
            RequestBody,
            ResponseBody,
            TipoComandoTef,
            BufferTef
        }

        public IEnumerable<Log> Logs { get; internal set; }

        public FrmDetalhe()
        {
            InitializeComponent();
        }

        private void FrmDetalhe_Load(object sender, EventArgs e)
        {
            FormatarLvwRequests();
            ListarDados();
        }

        private void ListarDados()
        {
            lvwLogs.Items.Clear();
            Log logAnterior = null;

            foreach (var log in Logs)
            {

                var item = new ListViewItem
                {
                    Text = log.DataHora.ToString("dd/MM/yyyy HH:mm:ss fff")
                };
                item.SubItems.Add((log.DuracaoMilliSeconds).ToString("#,##0"));
                item.SubItems.Add(log.ObterGap(logAnterior).TotalMilliseconds.ToString("#,##0"));
                item.SubItems.Add(log.RequestMethod);
                item.SubItems.Add(log.RequestPath);
                item.SubItems.Add(log.RequestBody);
                item.SubItems.Add(log.ResponseBody);

                if (!String.IsNullOrEmpty(log.ResponseBody))
                {
                    var data = JObject.Parse(log.ResponseBody);
                    AddFromJson(item, data, "tipoComando");
                    AddFromJson(item, data, "buffer");
                }

                lvwLogs.Items.Add(item);

                logAnterior = log;
            }
        }

        private void AddFromJson(ListViewItem item, JObject data, string propriedade)
        {
            var dado = "";
            var tipoComando = data.SelectToken(propriedade);
            if (tipoComando != null)
                dado = tipoComando.Value<string>();
            item.SubItems.Add(dado);
        }

        private void FormatarLvwRequests()
        {
            AplicarFormatacaoBasicaListView(lvwLogs);
            lvwLogs.Columns.Add("Data/Hora", 200);
            lvwLogs.Columns.Add("Duração (ms)", 100, HorizontalAlignment.Right);
            lvwLogs.Columns.Add("Gap (ms)", 70, HorizontalAlignment.Right);
            lvwLogs.Columns.Add("Método", 70);
            lvwLogs.Columns.Add("Request", 400);
            lvwLogs.Columns.Add("Request Body", 0);
            lvwLogs.Columns.Add("Response Body", 0);
            lvwLogs.Columns.Add("Tipo Comando TEF", 400);
            lvwLogs.Columns.Add("Buffer TEF", 400);
        }

        private void AplicarFormatacaoBasicaListView(ListView listView)
        {
            listView.Columns.Clear();
            listView.View = View.Details;
            listView.MultiSelect = false;
            listView.FullRowSelect = true;
        }

        private void lvwLogs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwLogs.SelectedItems.Count == 0)
                return;

            var requestBody = lvwLogs.SelectedItems[0].SubItems[(int)Colunas.RequestBody].Text;
            var responseBody = lvwLogs.SelectedItems[0].SubItems[(int)Colunas.ResponseBody].Text;

            if (String.IsNullOrEmpty(requestBody) || String.IsNullOrEmpty(responseBody))
                return;

            var json = "{\"requestBody\":" + requestBody + "," +
                "\"responseBody\":" + responseBody + "}";

            var jsonService = new JsonService();

            json = jsonService.Formatar(json);

            MessageBox.Show(json, "Request", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmDetalhe_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
