using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cancer_system.Models.Entities
{
    public class ChatbotMessage
    {
        [Key]
        public int MessageId { get; set; }

        [Required]
        public int SessionId { get; set; }

        [ForeignKey(nameof(SessionId))]
        public ChatbotSession Session { get; set; }

        [Required]
        [MaxLength(20)]
        public string Role { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int AnswerIndex { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
