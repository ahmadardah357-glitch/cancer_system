using System;
using System.Collections.Generic;

namespace cancer_system.DTOs.Chatbot
{
    
    public class ChatbotSendMessageDto
    {
        public string Message { get; set; }
    }

   
    public class ChatbotMessageDto
    {
        public string Role { get; set; }       
        public string Content { get; set; }
        public int AnswerIndex { get; set; }   
        public DateTime CreatedAt { get; set; }
    }

    public class ChatbotSessionDto
    {
        public int SessionId { get; set; }
        public string Title { get; set; }
        public string UserType { get; set; }  
        public List<ChatbotMessageDto> Messages { get; set; }
    }
}
