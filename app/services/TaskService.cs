using FileProcessor.Queues.Generic;
using Microsoft.Extensions.Logging;
using FileProcessor.Tasks;
using FileProcessor.Entities;

namespace FileProcessor.Services
{

    class TasksService : ITasksService
    {
        private ILogger _logger;
        private AsyncQueue<string> _queue;
        private FileSchema _fileSchema;
        private IAppSettings _config;

        public TasksService(ILoggerFactory loggerFactory, FileSchema fileSchema, IAppSettings config)
        {
            _logger = loggerFactory.CreateLogger<TasksService>();

            _queue = new AsyncQueue<string>();

            _fileSchema = fileSchema;

            _config = config;
        }

        public void Enqueue(string task)
        {
            _logger.LogInformation($"Enqueuing task: {task}");
            _queue.Enqueue(task);
        }


        public async void processTasks()
        {
            await foreach (string task in _queue)
            {
                try
                {
                    _logger.LogInformation($"processingTask: {task}");
                    ProcessFileTask.Execute(task, _fileSchema, _logger, _config);
                    _logger.LogInformation($"Finish processingTask: {task}");
                }
                catch (System.Exception e)
                {
                    _logger.LogError($"Error Processing File: {task} Exception: {e.Message}");
                    throw;
                }
            }

        }
    }

}
