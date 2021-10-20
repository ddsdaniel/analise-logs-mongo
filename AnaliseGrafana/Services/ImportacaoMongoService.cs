using AnaliseGrafana.Abstractions.Services;
using AnaliseGrafana.Factories;
using AnaliseGrafana.Models;
using AnaliseGrafana.Models.TorusPDV;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnaliseGrafana.Services
{
    public class ImportacaoMongoService : IImportacaoService
    {
        private readonly DateTime _dataInicial;
        private readonly DateTime _dataFinal;
        private readonly string _criterio;

        public ImportacaoMongoService(DateTime dataInicial, DateTime dataFinal, string criterio)
        {
            _dataInicial = dataInicial;
            _dataFinal = dataFinal;
            _criterio = criterio;
        }

        public IEnumerable<Log> Importar()
        {
            var collectionFactory = new CollectionFactory();
            var collection = collectionFactory.Criar<LogTorusPDV>("logs");

            var logsTorus = collection
                .AsQueryable()
                .Where(l => !l.Properties.RequestPath.EndsWith("Hub") &&
                            l.Timestamp >= _dataInicial &&
                            l.Timestamp <= _dataFinal &&
                            l.Properties.RequestPath.Contains(_criterio) &&
                            l.Level != "Error"
                            )
                .ToList();

            var logs = logsTorus
                .Select(l => new Log(l.Timestamp, l.Properties.RequestPath, l.Properties.RequestTime, l.Properties.RequestMethod))
                .ToList();

            return logs;

        }
    }

}
