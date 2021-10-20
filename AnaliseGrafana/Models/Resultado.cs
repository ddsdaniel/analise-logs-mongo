namespace AnaliseGrafana.Models
{
    public class Resultado
    {
        public Intervalo Intervalo { get; private set; }
        public int Ocorrencias { get; private set; }
        public decimal Percentual { get; private set; }
        public decimal PercentualAcumulado { get; private set; }

        public Resultado(Intervalo intervalo, int ocorrencias, decimal percentual, decimal percentualAcumulado)
        {
            Intervalo = intervalo;
            Ocorrencias = ocorrencias;
            Percentual = percentual;
            PercentualAcumulado = percentualAcumulado;
        }

        public override string ToString() => $"{Intervalo.Inicial} - {Intervalo.Final} seg: {Ocorrencias}, {Percentual:#0.0}%, 0 - {Intervalo.Final} seg: {PercentualAcumulado:#0.0}%";
    }
}
