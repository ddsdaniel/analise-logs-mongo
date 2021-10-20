namespace AnaliseGrafana.Models
{
    public class Intervalo
    {
        public int Inicial { get; private set; }
        public int Final { get; private set; }

        public Intervalo(int inicial, int final)
        {
            Inicial = inicial;
            Final = final;
        }

        public override string ToString() => $"De {Inicial} até {Final}";
    }
}
