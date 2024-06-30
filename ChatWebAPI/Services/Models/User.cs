using System.ComponentModel.DataAnnotations;

namespace ChatWeb.Core.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string HashedPassword { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Message> ReceivedMessages { get; set; }
        public ICollection<Connection> Connections { get; set; }
    }
}
