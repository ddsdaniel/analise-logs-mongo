using MongoDB.Driver;

namespace AnaliseGrafana.Factories
{
    public class CollectionFactory
    {
        public IMongoCollection<T> Criar<T>(string nome)
        {
            var client = new MongoClient("mongodb://localhost:27017/pdv?replicaSet=rs1");
            var database = client.GetDatabase("pdv-logs");
            var collection = database.GetCollection<T>(nome);
            return collection;
        }
    }
}
