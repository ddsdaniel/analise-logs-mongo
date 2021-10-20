using AnaliseGrafana.Models;
using System.Collections.Generic;

namespace AnaliseGrafana.Abstractions.Services
{
    public interface IImportacaoService
    {
        public IEnumerable<Log> Importar();
    }
}
