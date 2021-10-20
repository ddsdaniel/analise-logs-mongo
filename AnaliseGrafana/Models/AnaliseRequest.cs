namespace AnaliseGrafana.Models
{
    public class AnaliseRequest
    {
        public string RequestMethod { get; private set; }
        public string RequestPath { get; private set; }
        public double TempoMedio { get; private set; }
        public int Ocorrencias { get; private set; }
        public double TempoMinimo { get; private set; }
        public double TempoMaximo { get; private set; }
        public AnaliseRequest(string requestPath, double tempoMedio, string requestMethod, int ocorrencias, double tempoMinimo, double tempoMaximo)
        {
            RequestPath = requestPath;
            TempoMedio = tempoMedio;
            RequestMethod = requestMethod;
            Ocorrencias = ocorrencias;
            TempoMinimo = tempoMinimo;
            TempoMaximo = tempoMaximo;
        }

    }
}
