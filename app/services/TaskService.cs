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

        public TasksService(ILoggerFactory loggerFactory, FileSchema fileSchema)
        {
            _logger = loggerFactory.CreateLogger<TasksService>();

            _queue = new AsyncQueue<string>();

            _fileSchema = fileSchema;
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
                _logger.LogInformation($"processingTask: {task}");
                new ProcessFileTask(task, _fileSchema);
            }

        }
    }

}
