namespace TaskTideAPI.Models
{
    public class Calendar : ITransactionItem
    {
        public int Id { get; set; }

        public User Owner { get; set; }

        public string Name { get; set; }

        public TaskEventColor? Color { get; set; }

        public ICollection<TaskEvent> TasksAndEvents { get; set; }

        /// <summary>
        /// Invitation for other users to access the calendar
        /// </summary>
        public ICollection<CalendarInvitation> Invitations { get; set; }

        /// <summary>
        /// Users that have access to the calendar
        /// </summary>
        public ICollection<User> SharedWith { get; set; }

        /// <summary>
        /// Represents if people who the calendar is shared with can edit the calendar
        /// </summary>
        public bool IsReadOnly { get; set; } = true;
    }
}
