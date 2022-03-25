using Mongo.CSharp.TestBed.Worker.Entities;
using Mongo.CSharp.TestBed.Worker.Repositories;

namespace Mongo.CSharp.TestBed.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IWorkplanExportJobRepository _repository;
        private readonly ILogger<Worker> _logger;

        public Worker(IWorkplanExportJobRepository repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<Worker>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            
            // job is kicked off (from queue from example)
            // create an export job with no workplan (but Id is known)
            var exportJob = new ExportJob(Guid.NewGuid())
            {
                WorkplanId = 24
            };

            await _repository.AddAsync(exportJob).ConfigureAwait(false); ;
            
            // first api call retrieves the core data and inserts the workplan into the document
            var workplan = new Workplan(24, "Test Workplan")
            {
                Sections = new[]
                {
                    new WorkplanSection(31, "Section A")
                    {
                        Tasks = new[]
                        {
                            new WorkplanTask(41, "Task X1", TaskType.Standard),
                            new WorkplanTask(42, "Task X2", TaskType.Clerical)
                        }
                    },
                    new WorkplanSection(32, "Section B")
                    {
                        Tasks = new[]
                        {
                            new WorkplanAnomalyTask(51, "Task Y1"),
                            new WorkplanTask(52, "Task Y2", TaskType.Standard)
                        }
                    }
                }
            };

            await _repository.InsertWorkplanAsync(exportJob.Id, workplan).ConfigureAwait(false);
            
            // foreach task where the type is anomaly, we'll head off to get some anomaly details. on return, we need to update the Anomaly Details field
            var anomalyDetails = new AnomalyDetails("AN_002", "Standard anomaly issue");

            await _repository.UpdateAnomalyDetailsAsync(exportJob.Id, 32, 51, anomalyDetails).ConfigureAwait(false);
        }
    }
}