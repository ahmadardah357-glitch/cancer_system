using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cancer_system.Models.Entities
{
    public class ChatbotSession
    {
        [Key]
        public int SessionId { get; set; }

        [Required]
        public string UserId { get; set; }   

        [Required]
        public string UserType { get; set; } 

        [Required]
        public string Title { get; set; }

        public string ModelName { get; set; } = "gpt";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<ChatbotMessage> Messages { get; set; }
    }
}
