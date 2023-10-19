using System.ComponentModel.DataAnnotations;

namespace TaskTideAPI.DTO
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "User name must be at least 2 characters long")]
        [MaxLength(32, ErrorMessage = "User name must be at most 32 characters long")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can't contain any special characters")]
        public string Username { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(64, ErrorMessage = "Password must be at most 64 characters long")]
        [RegularExpression(@"(?=.*[a-zA-Z])(?=.*\d).{8,64}", ErrorMessage = "Password must contain at least one digit and one letter")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Password and password confirmation must match")]
        public string PasswordConfirmation { get; set; }
    }
}
