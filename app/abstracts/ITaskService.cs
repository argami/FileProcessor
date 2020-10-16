
namespace FileProcessor.Services
{

    public interface ITasksService
    {
        void Enqueue(string task);
        void processTasks();

    }

}
