using Newtonsoft.Json;

namespace TaskTideAPI.Models
{
    public class User : ITransactionItem
    {
        public int Id { get; set; }

        public string Username { get; set; }

        [JsonIgnore]
        public string HashedPassword { get; set; }

        public ICollection<Calendar> Calendars { get; set; } = new List<Calendar>();

        public ICollection<Calendar> SharedCalendars { get; set; } = new List<Calendar>();
     
        public ICollection<CalendarInvitation> InvitationsReceived { get; set; }

        public ICollection<CalendarInvitation> InvitationsSent { get; set; }
    }
}
