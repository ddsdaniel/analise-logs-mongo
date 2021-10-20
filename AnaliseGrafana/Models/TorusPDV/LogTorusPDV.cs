using System;

namespace AnaliseGrafana.Models.TorusPDV
{
    public class LogTorusPDV
    {
        //public string _id { get; set; }
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string MessageTemplate { get; set; }
        public string RenderedMessage { get; set; }
        public Properties Properties { get; set; }
        public Renderings Renderings { get; set; }
        public string UtcTimestamp { get; set; }
    }
}
