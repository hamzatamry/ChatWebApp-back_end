using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatWeb.Core.Models
{
    public class Connection
    {
        [Key]
        public string ConnectionId { get; set; } //signalR connectionId after login

        [Required]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public DateTime ConnectedAt { get; set; } = DateTime.Now;

        [DatabaseGenerated(DatabaseGeneratedOption.None)] //preventing EF core from autofillig this field (gonna be filled on logout)
        public DateTime? DisconnectedAt { get; set; }
    }
}
