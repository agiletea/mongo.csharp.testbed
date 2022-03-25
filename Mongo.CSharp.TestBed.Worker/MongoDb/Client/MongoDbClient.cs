using MongoDB.Driver;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Client
{
    [ExcludeFromCodeCoverage]
    internal sealed class MongoDbClient : MongoClient, IMongoDbClient
    {
        public MongoDbClient(string dbConnection)
            : base(dbConnection)
        {
        }
    }
}
