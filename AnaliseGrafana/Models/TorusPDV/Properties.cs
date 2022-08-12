using System;
using System.Collections.Generic;
using System.Text;

namespace AnaliseGrafana.Models.TorusPDV
{
    public class Properties
    {
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string QueryString { get; set; }
        public int StatusCode { get; set; }
        public double RequestTime { get; set; }
        public string ResponseBody { get; set; }
        //public ResponseHeaders ResponseHeaders { get; set; }
        public string ResponseHeaders { get; set; }
        public string RequestBody { get; set; }
        public RequestQuery RequestQuery { get; set; }
        //public RequestHeaders RequestHeaders { get; set; }
        public string RequestHeaders { get; set; }
        public string Identificacao { get; set; }
        public string RequestId { get; set; }
        public string SpanId { get; set; }
        public string TraceId { get; set; }
        public string ParentId { get; set; }
        public string ConnectionId { get; set; }
        public string ProjectVersion { get; set; }
        public string OS { get; set; }
        public string MachineName { get; set; }
        public string EnvironmentUserName { get; set; }
    }
}
