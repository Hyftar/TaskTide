using NodaTime;

namespace TaskTideAPI.Models
{
    public class CalendarInvitation : ITransactionItem
    {
        public int Id { get; set; }

        public User Inviter { get; set; }

        public User Invitee { get; set; }

        public Calendar Calendar { get; set; }

        public bool? Accepted { get; set; }

        public ZonedDateTime? RespondedAt { get; set; }

        public ZonedDateTime CreatedAt { get; set; }

        public bool Deleted { get; set; } = false;
    }
}
