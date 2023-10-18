using NodaTime;
using TaskTideAPI.Models;

namespace TaskTideAPI.DTO
{
    public class AddTaskDTO
    {
        public int CalendarId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public LocalDate StartDate { get; set; }

        public LocalTime? Duration { get; set; }

        public LocalTime? StartTime { get; set; }

        public bool AllDay { get; set; }

        public ICollection<RecurrenceDTO> Recurrences { get; set; }
    }
}
