using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks.Dataflow;
using FileProcessor.Queues.Generic;
using Microsoft.Extensions.Logging;

namespace FileProcessor.Services
{

    public interface ITasksService
    {
        void Enqueue(string task);
        void processTasks();

    }

}
