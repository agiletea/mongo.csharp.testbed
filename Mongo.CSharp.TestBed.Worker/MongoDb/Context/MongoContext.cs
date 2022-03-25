using Mongo.CSharp.TestBed.Worker.MongoDb.Client;
using MongoDB.Driver;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Context
{
    internal class MongoContext : IMongoContext
    {
        private readonly IClientProvider clientProvider;
        private readonly List<Func<Task>> commands;
        private readonly ILogger<MongoContext> logger;

        public MongoContext(
            IClientProvider clientProvider,
            ILoggerFactory loggerFactory)
        {
            this.clientProvider = clientProvider;
            logger = loggerFactory.CreateLogger<MongoContext>();

            // every command will be stored and it'll be processed at SaveChanges
            commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChangesAsync()
        {
            CheckMongo();

            await Task.WhenAll(commands.Select(c => c()));

            return commands.Count;
        }

        public IMongoCollection<TDocument> GetCollection<TDocument>(string name)
        {
            CheckMongo();

            // we know the app will throw an exception if the previous statement fails to deliver
            return clientProvider.Database!.GetCollection<TDocument>(name);
        }

        public void AddCommand(Func<Task> func)
        {
            commands.Add(func);
        }

        private void CheckMongo()
        {
            if (clientProvider.Client == null)
            {
                logger.LogError("Mongo check failed. Client is null");
                throw new InvalidOperationException("Mongo check failed. Client is null");
            }

            if (clientProvider.Database == null)
            {
                logger.LogError("Mongo check failed. Database is null");
                throw new InvalidOperationException("Mongo check failed. Database is null");
            }
        }
    }
}