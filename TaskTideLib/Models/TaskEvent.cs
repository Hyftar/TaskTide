namespace TaskTideLib.Models
{
    public class TaskEvent
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public bool AllDay { get; set; }

        public ICollection<Recurrence> Recurrences { get; set; }
    }
}
