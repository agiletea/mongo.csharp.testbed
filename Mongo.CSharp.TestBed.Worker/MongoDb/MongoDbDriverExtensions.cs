using Mongo.CSharp.TestBed.Worker.MongoDb.Client;
using Mongo.CSharp.TestBed.Worker.MongoDb.Context;

namespace Mongo.CSharp.TestBed.Worker.MongoDb
{
    /// <summary>
    /// <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/> extensions for registering the services
    /// specific to the Mongo Db Persistence library
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class MongoDbDriverExtensions
    {
        /// <summary>
        /// Sets up services for the Mongo Db Persistence library
        /// </summary>
        /// <param name="services">The <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/></param>
        /// <param name="options">The mongo options to be used for configuration</param>
        /// <exception cref="ArgumentNullException">Thrown if options are null</exception>
        public static void AddMongo(
            this IServiceCollection services,
            Action<MongoOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            services.AddSingleton<IClientProvider, ClientProvider>();
            services.AddScoped<IMongoContext, MongoContext>();
            services.Configure(options);
        }
    }
}
