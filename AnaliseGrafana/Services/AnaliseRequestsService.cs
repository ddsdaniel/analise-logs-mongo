﻿using AnaliseGrafana.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AnaliseGrafana.Services
{
    public class AnaliseRequestsService
    {
        public IEnumerable<AnaliseRequest> Analisar(IEnumerable<Log> logs)
        {
            var analise = logs
                .Select(l => new Log(l.DataHora, Padronizar(l.RequestPath), l.DuracaoMilliSeconds, l.RequestMethod, l.RequestBody, l.ResponseBody))
                .GroupBy(l => new
                {
                    l.RequestPath,
                    l.RequestMethod
                })
                .Select(g => new AnaliseRequest(
                    g.First().RequestPath,
                    g.Average(l => l.DuracaoMilliSeconds) / 1000,
                    g.First().RequestMethod,
                    g.Count(),
                    g.Min(l => l.DuracaoMilliSeconds) / 1000,
                    g.Max(l => l.DuracaoMilliSeconds) / 1000
                    ))
                .OrderByDescending(x => x.TempoMedio)
                .ToList();

            return analise;
        }

        private string Padronizar(string valor)
        {
            valor = PadronizarGuid(valor);
            valor = PadronizarNumeros(valor);

            return valor;
        }

        private static string PadronizarGuid(string valor)
        {
            if (!String.IsNullOrEmpty(valor))
                valor = Regex.Replace(valor, @"(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}", Guid.Empty.ToString());

            return valor;
        }

        private static string PadronizarNumeros(string valor)
        {
            const string PATTERN = @"(\/\d+\/)|(\/\d+$)";

            if (!String.IsNullOrEmpty(valor))
                valor = Regex.Replace(valor, PATTERN, "/#");

            return valor;
        }
    }
}
