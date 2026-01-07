namespace cancer_system.DTOs.ChatDTO
{
    public class ChatListDto
    {
        public string OtherUserId { get; set; }
        public string OtherUserName { get; set; }
        public string LastMessage { get; set; }
        public DateTime LastSentAt { get; set; }
    }
}
