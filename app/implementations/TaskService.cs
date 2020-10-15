using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using FileProcessor.Queues.Generic;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Services
{

    class TasksService : ITasksService
    {
        private ILogger _logger;
        private AsyncQueue<string> _queue;

        public TasksService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TasksService>();

            _queue = new AsyncQueue<string>();
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
            }

        }
    }

}
