using AnaliseGrafana.Abstractions.Services;
using AnaliseGrafana.Factories;
using AnaliseGrafana.Models;
using AnaliseGrafana.Models.TorusPDV;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnaliseGrafana.Services
{
    public class ImportacaoMongoService : IImportacaoService
    {
        private readonly Filtro _filtro;

        public ImportacaoMongoService(Filtro filtro)
        {
            _filtro = filtro;
        }

        public IEnumerable<Log> Importar()
        {
            var collectionFactory = new CollectionFactory();
            var collection = collectionFactory.Criar<LogTorusPDV>("logs");

            var query = collection
                .AsQueryable()
                .Where(l => !l.Properties.RequestPath.EndsWith("Hub") &&
                            l.Timestamp >= _filtro.DataInicial &&
                            l.Timestamp <= _filtro.DataFinal &&
                            l.Level != "Error" &&
                            l.Properties.RequestMethod != null &&
                            l.Properties.RequestMethod != String.Empty
                            );

            if (!String.IsNullOrEmpty(_filtro.Criterio))
            {
                if (_filtro.Criterio.Contains(";"))
                {
                    var criterios = _filtro.Criterio.Split(';');

                    var regex = new Regex(String.Join('|', criterios));
                   
                    query = query.Where(l => regex.IsMatch(l.Properties.RequestPath));
                }
                else
                {
                    query = query.Where(l => l.Properties.RequestPath.Contains(_filtro.Criterio));
                }
            }

            if (!String.IsNullOrEmpty(_filtro.Metodo))
                query = query.Where(l => l.Properties.RequestMethod.Equals(_filtro.Metodo));

            var logsTorus = query
                .OrderBy(l => l.Timestamp)
                .ToList();

            var logs = logsTorus
                .Select(l => new Log(l.Timestamp, l.Properties.RequestPath, l.Properties.RequestTime, l.Properties.RequestMethod, l.Properties.RequestBody, l.Properties.ResponseBody))
                .ToList();

            return logs;

        }
    }

}
