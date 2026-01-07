using cancer_system.Models.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
{
    public class Chat
    {

        [Key]
        public int ChatId { get; set; }

        [Required]
        [Column("sender_id")]
        public string SenderId { get; set; }

        [Required]
        [Column("receiver_id")]
        public string ReceiverId { get; set; }

        [Required]
        public string MessageText { get; set; }

        public DateTime SentAt { get; set; }

       
        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; }

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; }


    }
}
