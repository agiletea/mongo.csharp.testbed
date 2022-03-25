using MongoDB.Driver;

namespace Mongo.CSharp.TestBed.Worker.MongoDb.Context
{
    /// <summary>
    /// Provides a specific DbContext for Mongo to allow access to a mongo collection
    /// </summary>
    public interface IMongoContext : IDbContext
    {
        /// <summary>
        /// Gets a mongo collection for the given document type
        /// </summary>
        /// <param name="name">Name of the document type</param>
        /// <typeparam name="TDocument">Document type</typeparam>
        /// <returns>A mongo collection of type <c>TDocument</c></returns>
        IMongoCollection<TDocument> GetCollection<TDocument>(string name);
    }
}
