namespace TaskManagerApp
{
    public class TaskItem : BaseTask
    {
        public override string GetDetails()
        {
            return $"[Task] {Title} | Due: {DueDate:dd/MM/yyyy} | Category: {Category} | Done: {IsComplete}";
        }
    }
}
