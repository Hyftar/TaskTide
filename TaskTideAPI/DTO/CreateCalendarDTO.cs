using System.ComponentModel.DataAnnotations;

namespace TaskTideAPI.DTO
{
    public class CreateCalendarDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "Calendar name must be at most 50 characters long")]
        public string Name { get; set; }
    }
}
