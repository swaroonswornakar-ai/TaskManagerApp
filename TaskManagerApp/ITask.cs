namespace TaskManagerApp
{
    public interface ITask
    {
        string GetDetails();
        void MarkComplete();
    }
}