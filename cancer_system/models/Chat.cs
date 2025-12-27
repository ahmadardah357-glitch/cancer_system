using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.models
{
    public class Chat
    {
        [Key]
        [Column("chat_id")]
        public int ChatId { get; set; }

        // FK → AspNetUsers.Id (Sender)
        [Required]
        [StringLength(450)]
        [Column("sender_id")]
        public string SenderId { get; set; }

        // FK → AspNetUsers.Id (Receiver)
        [Required]
        [StringLength(450)]
        [Column("receiver_id")]
        public string ReceiverId { get; set; }

        [Required]
        [Column("message_text")]
        public string MessageText { get; set; }

        [Required]
        [Column("sent_at")]
        public DateTime SentAt { get; set; }
    }
}
