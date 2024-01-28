using System.ComponentModel.DataAnnotations;

namespace ContactDesktop.Models
{
    public class UserRegistration
    {
        [Required, MaxLength(30)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
