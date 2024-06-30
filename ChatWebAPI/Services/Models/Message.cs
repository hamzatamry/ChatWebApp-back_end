using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SenderId { get; set; }

        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        [Required]
        public int ReceiverId { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}
