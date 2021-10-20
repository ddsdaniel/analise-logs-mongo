using System;

namespace AnaliseGrafana.Models
{
    public class Log
    {
        public DateTime DataHora { get; private set; }
        public string RequestPath { get; private set; }
        public string RequestMethod { get; private set; }
        public double Duracao { get; private set; }

        protected Log() { }

        public Log(DateTime dataHora, string requestPath, double duracao, string requestMethod)
        {
            DataHora = dataHora;
            RequestPath = requestPath;
            Duracao = duracao;
            RequestMethod = requestMethod;
        }

        public override string ToString() => $"{DataHora:dd/MM/yyyy HH:mm:ss} [{RequestMethod}] {RequestPath}: {Duracao}ms";
    }
}
