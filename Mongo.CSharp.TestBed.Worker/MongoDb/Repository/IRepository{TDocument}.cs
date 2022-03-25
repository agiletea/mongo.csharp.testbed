using Mongo.CSharp.TestBed.Worker.MongoDb.Entities;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Repository
{
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1649:File name should match first type name",
        Justification = "Differentiating generic version of interface")]
    public interface IRepository<TDocument, TId> : IRepository
        where TDocument : IIndexedEntity<TId>
    {
        Task AddAsync(TDocument document);
        Task<TDocument?> GetByIdAsync(TId id);
        TDocument? GetById(TId id);
        Task<IEnumerable<TDocument>> GetAllAsync();
        IEnumerable<TDocument> GetAll();
        Task ReplaceAsync(TDocument document);
        Task RemoveAsync(TId id);
    }
}
