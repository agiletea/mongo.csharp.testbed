using Mongo.CSharp.TestBed.Worker;
using Mongo.CSharp.TestBed.Worker.MongoDb;
using Mongo.CSharp.TestBed.Worker.MongoDb.Enums;
using Mongo.CSharp.TestBed.Worker.Repositories;
using MongoDB.Bson;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging();
        services.AddMongo(options =>
        {
            options.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            options.EnumRepresentation = EnumRepresentation.Numeric;
            options.DbConnection = hostContext.Configuration["mongo:dbConnection"];
            options.DbName = hostContext.Configuration["mongo:dbName"];
        });
        services.AddSingleton<IWorkplanExportJobRepository, WorkplanExportJobRepository>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
