using System.ComponentModel.DataAnnotations;

namespace OnlineOrganizationManagementSystem.Models.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
