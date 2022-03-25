using Mongo.CSharp.TestBed.Worker.MongoDb.Context;
using Mongo.CSharp.TestBed.Worker.MongoDb.Entities;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Repository
{
    [SuppressMessage(
        "StyleCop.CSharp.DocumentationRules",
        "SA1649:File name should match first type name",
        Justification = "File name allows for clarification that this is a generic class")]
    public abstract class RepositoryBase<TDocument, TContext, TId> : IRepository<TDocument, TId>
        where TContext : IDbContext
        where TDocument : IIndexedEntity<TId>
    {
        private readonly TContext context;
        private string collectionName;

        protected RepositoryBase(TContext context)
        {
            this.context = context;
            collectionName = typeof(TDocument).Name;
        }

        public IDbContext DbContext => context;

        public virtual string CollectionName
        {
            get => collectionName;
            protected set => collectionName = value;
        }

        public abstract Task AddAsync(TDocument document);

        public abstract Task<TDocument?> GetByIdAsync(TId id);

        public abstract TDocument? GetById(TId id);

        public abstract Task<IEnumerable<TDocument>> GetAllAsync();

        public abstract IEnumerable<TDocument> GetAll();

        public abstract Task ReplaceAsync(TDocument document);

        public abstract Task RemoveAsync(TId id);
    }
}
