namespace Mongo.CSharp.TestBed.Worker.MongoDb.Context
{
    public interface IDbContext
    {
        void AddCommand(Func<Task> func);
        Task<int> SaveChangesAsync();
    }
}
