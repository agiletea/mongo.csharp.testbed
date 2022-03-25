using Mongo.CSharp.TestBed.Worker.Entities;
using Mongo.CSharp.TestBed.Worker.MongoDb.Context;
using Mongo.CSharp.TestBed.Worker.MongoDb.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mongo.CSharp.TestBed.Worker.Repositories
{
    internal class WorkplanExportJobRepository : DocumentRepositoryBase<ExportJob, Guid>, IWorkplanExportJobRepository
    {
        public WorkplanExportJobRepository(IMongoContext context, ILoggerFactory loggerFactory)
            : base(context, loggerFactory.CreateLogger<WorkplanExportJobRepository>())
        {
        }

        public async Task InsertWorkplanAsync(Guid jobId, Workplan workplan)
        {
            var filter = Builders<ExportJob>.Filter.Eq(doc => doc.Id, jobId);
            var update = Builders<ExportJob>.Update.Set(x => x.Workplan, workplan);

            await GetDbSet().UpdateOneAsync(filter, update);
        }

        public async Task UpdateAnomalyDetailsAsync(Guid jobId, int sectionId, int taskId, AnomalyDetails anomalyDetails)
        {
            //var filter = Builders<ExportJob>.Filter.Eq(x => x.Id, jobId) &
            //             Builders<ExportJob>.Filter.ElemMatch(x => x.Workplan!.Sections, Builders<WorkplanSection>.Filter.Eq(x => x.Id, sectionId)) &
            //             Builders<ExportJob>.Filter.ElemMatch(x => x.Workplan!.Sections, Builders<WorkplanSection>.Filter.ElemMatch(x => x.Tasks,
            //                 Builders<WorkplanTask>.Filter.Eq(x => x.Id, taskId)));

            //var update = Builders<ExportJob>.Update.Set("Workplan.Sections.$[].Tasks.$[].AnomalyDetails", anomalyDetails);


            //await GetDbSet().UpdateOneAsync(filter, update).ConfigureAwait(false);

            var filter = Builders<ExportJob>.Filter.Eq(x => x.Id, jobId);

            var update = Builders<ExportJob>.Update.Set("Workplan.Sections.$[s].Tasks.$[t].AnomalyDetails", anomalyDetails);

            var arrayFilters = new[]
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(
                    new BsonDocument("s._id", new BsonDocument("$in", new BsonArray(new [] { sectionId })))),
                new BsonDocumentArrayFilterDefinition<BsonDocument>(
                    new BsonDocument("t._id", new BsonDocument("$in", new BsonArray(new [] { taskId }))))
            };

            await GetDbSet().UpdateOneAsync(filter, update, new UpdateOptions { ArrayFilters = arrayFilters }).ConfigureAwait(false);
        }
    }

    public interface IWorkplanExportJobRepository : IRepository<ExportJob, Guid>
    {
        Task InsertWorkplanAsync(Guid jobId, Workplan workplan);

        Task UpdateAnomalyDetailsAsync(Guid jobId, int sectionId, int taskId, AnomalyDetails anomalyDetails);
    }
}
