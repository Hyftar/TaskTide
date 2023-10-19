using System.ComponentModel.DataAnnotations;

namespace TaskTideAPI.DTO
{
    public class AddTaskDTO
    {
        [Required]
        public int CalendarId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public DateOnly StartDate { get; set; }

        public int? DurationInMinutes { get; set; }

        public TimeOnly? StartTime { get; set; }

        [Required]
        public bool AllDay { get; set; }

        [Required]
        public ICollection<RecurrenceDTO> Recurrences { get; set; }
    }
}
