namespace TaskManagerApp
{
    public class UrgentTask : BaseTask
    {
        public string PriorityLevel { get; set; } = "High";

        public override string GetDetails()
        {
            return $"[URGENT] {Title} | Due: {DueDate:dd/MM/yyyy} | Priority: {PriorityLevel} | Done: {IsComplete}";
        }
    }
}
