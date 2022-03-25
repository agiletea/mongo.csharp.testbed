using Mongo.CSharp.TestBed.Worker.MongoDb.Context;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Repository
{
    public interface IRepository
    {
        IDbContext DbContext { get; }
    }
}
