using System.ComponentModel.DataAnnotations;

namespace OnlineOrganizationManagementSystem.Models
{
    public class Messages
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        public string SenderMail { get; set; }
        [Required]
        public string ReceiverMail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Status { get; set; }

    }
}
