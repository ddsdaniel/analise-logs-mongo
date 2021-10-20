using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnaliseGrafana.Models.TorusPDV
{
    public class RequestHeaders
    {
        [JsonProperty("Cache-Control")]
        public string CacheControl { get; set; }
        public string Connection { get; set; }
        public string Pragma { get; set; }
        public string Upgrade { get; set; }

        [JsonProperty("Accept-Encoding")]
        public string AcceptEncoding { get; set; }

        [JsonProperty("Accept-Language")]
        public string AcceptLanguage { get; set; }
        public string Host { get; set; }

        [JsonProperty("User-Agent")]
        public string UserAgent { get; set; }
        public string Origin { get; set; }

        [JsonProperty("Sec-WebSocket-Version")]
        public string SecWebSocketVersion { get; set; }

        [JsonProperty("Sec-WebSocket-Key")]
        public string SecWebSocketKey { get; set; }

        [JsonProperty("Sec-WebSocket-Extensions")]
        public string SecWebSocketExtensions { get; set; }
    }
}
