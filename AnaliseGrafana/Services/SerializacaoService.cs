using System.IO;
using System.Xml.Serialization;

namespace AnaliseGrafana.Services
{
    public class SerializacaoService
    {
        public T Desserializar<T>(string caminho)
            where T : class
        {
            if (!File.Exists(caminho))
                return null;

            XmlSerializer formatador = new XmlSerializer(typeof(T));

            string conteudoConfigXml = File.ReadAllText(caminho);

            using StringReader reader = new StringReader(conteudoConfigXml);

            T obj = (T)formatador.Deserialize(reader);

            reader.Close();

            return obj;
        }

        public void Serializar<T>(string caminho, T objeto)
            where T : class
        {
            XmlSerializer formatador = new XmlSerializer(objeto.GetType());

            FileStream fluxo = File.Create(caminho);
            
            formatador.Serialize(fluxo, objeto);

            fluxo.Close();
        }

    }
}
