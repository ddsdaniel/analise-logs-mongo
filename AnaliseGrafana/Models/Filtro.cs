using System;

namespace AnaliseGrafana.Models
{
    public class Filtro
    {
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public string Criterio { get; set; }
        public string Metodo { get; set; }
    }
}
