using AnaliseGrafana.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnaliseGrafana.Services
{
    public class AnaliseService
    {
       
        public IEnumerable<Resultado> Analisar(IEnumerable<Log> logs)
        {
            var intervaloFinal = Math.Truncate(logs.Max(l => l.DuracaoMilliSeconds) / 1000) + 1;
            var ocorrenciasAcumuladas = 0;

            var resultados = new List<Resultado>();
            for (int i = 0; i <= intervaloFinal; i++)
            {
                var intervalo = new Intervalo(i, i + 1);
                var ocorrencias = logs.Count(l => l.DuracaoMilliSeconds >= intervalo.Inicial * 1000 && l.DuracaoMilliSeconds <= intervalo.Final * 1000);
                if (ocorrencias == 0) continue;

                ocorrenciasAcumuladas += ocorrencias;

                var percentual = ocorrencias / (decimal)logs.Count() * 100;
                var percentualAcumulado = ocorrenciasAcumuladas / (decimal)logs.Count() * 100;


                var resultado = new Resultado(intervalo, ocorrencias, percentual, percentualAcumulado);

                resultados.Add(resultado);
            }

            return resultados;
        }
        
    }
}
