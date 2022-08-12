using Newtonsoft.Json;

namespace AnaliseGrafana.Services
{
    internal class JsonService
    {
        public string Formatar(string json)
        {
            dynamic parsedJson = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }
    }
}
