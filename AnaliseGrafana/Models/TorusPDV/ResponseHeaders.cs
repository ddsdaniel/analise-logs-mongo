using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnaliseGrafana.Models.TorusPDV
{
    public class ResponseHeaders
    {
        public string Connection { get; set; }
        public string Date { get; set; }
        public string Upgrade { get; set; }
        public string Server { get; set; }

        [JsonProperty("Sec-WebSocket-Accept")]
        public string SecWebSocketAccept { get; set; }
    }
}
