namespace cancer_system.DTOs.ChatDTO
{
    public class ChatMessageDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string MessageText { get; set; }
        public DateTime SentAt { get; set; }
    }

}
