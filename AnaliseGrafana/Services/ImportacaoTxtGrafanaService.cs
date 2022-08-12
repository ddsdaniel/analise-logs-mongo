using AnaliseGrafana.Abstractions.Services;
using AnaliseGrafana.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnaliseGrafana.Services
{
    public class ImportacaoTxtGrafanaService : IImportacaoService
    {
        private readonly string _caminhoArquivo;

        public ImportacaoTxtGrafanaService(string caminhoArquivo)
        {
            _caminhoArquivo = caminhoArquivo;
        }

        public IEnumerable<Log> Importar()
        {
            var linhas = File.ReadAllLines(_caminhoArquivo);
            var logs = linhas
                .Where(linha => !String.IsNullOrEmpty(linha) && linha.Contains("RequestTime"))
                .Select(linha =>
                {
                    var campos = linha.Split('\t');
                    var dataHora = DateTime.Parse(campos[0]);
                    var requestPath = LerCampo(linha, "RequestPath", "");
                    var requestMethod = LerCampo(linha, "RequestMethod", "");
                    var requestTime = LerCampo(linha, "RequestTime");
                    var requestBody = LerCampo(linha, "RequestBody", "");
                    var responseBody = LerCampo(linha, "ResponseBody", "");

                    return new Log(dataHora, requestPath, requestTime, requestMethod, requestBody, responseBody);
                })
                .ToList();

            return logs;
        }

        private string LerCampo(string linha, string rotulo, string valorDefault)
        {
            var posicaoInicial = linha.IndexOf(rotulo);

            if (posicaoInicial < 0) return valorDefault;

            posicaoInicial += rotulo.Length + 2;

            var posicaoFinal = linha.IndexOf("\"", posicaoInicial);
            var tamanho = posicaoFinal - posicaoInicial;

            var valor = linha.Substring(posicaoInicial, tamanho);

            return valor;
        }

        private double LerCampo(string linha, string rotulo, double valorDefault = 0)
        {
            var posicaoInicial = linha.IndexOf(rotulo);

            if (posicaoInicial < 0) return valorDefault;

            posicaoInicial += rotulo.Length + 2;

            var posicaoFinal = linha.IndexOf(" ", posicaoInicial);

            var tamanho = posicaoFinal - posicaoInicial;

            var valor = linha
                .Substring(posicaoInicial, tamanho)
                .Replace(".", ",");

            return Convert.ToDouble(valor);
        }

    }
}
