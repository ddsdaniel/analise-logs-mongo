using System;

namespace AnaliseGrafana.Models
{
    public class Log
    {
        public DateTime DataHora { get; private set; }
        public string RequestPath { get; private set; }
        public string RequestMethod { get; private set; }
        public double DuracaoMilliSeconds { get; private set; }
        public string RequestBody { get; private set; }
        public string ResponseBody { get; private set; }

        protected Log() { }

        public Log(
            DateTime dataHora,
            string requestPath,
            double duracao,
            string requestMethod,
            string requestBody,
            string responseBody)
        {
            DataHora = dataHora;
            RequestPath = requestPath;
            DuracaoMilliSeconds = duracao;
            RequestMethod = requestMethod;
            RequestBody = requestBody;
            ResponseBody = responseBody;
        }

        public override string ToString() => $"{DataHora:dd/MM/yyyy HH:mm:ss} [{RequestMethod}] {RequestPath}: {DuracaoMilliSeconds}ms";

        public TimeSpan ObterGap(Log logAnterior)
        {
            if (logAnterior == null || String.IsNullOrEmpty(logAnterior.RequestPath))
                return default;

            var gap = DataHora - logAnterior.DataHora;

            gap = gap.Add(-TimeSpan.FromMilliseconds(DuracaoMilliSeconds));

            return gap;
        }
    }
}
