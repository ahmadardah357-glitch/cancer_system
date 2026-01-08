using cancer_system.Data;
using cancer_system.DTOs.Chatbot;
using cancer_system.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace cancer_system.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/chat")]
    public class ChatbotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatbotController(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                throw new UnauthorizedAccessException("UserId claim is missing");

            return userId;
        }

        private string GetUserType()
        {
            var role = User.FindFirstValue(ClaimTypes.Role);
            if (string.IsNullOrWhiteSpace(role))
                throw new UnauthorizedAccessException("Role claim is missing");

            return role; 
        }

        
        [HttpPost("session")]
        public async Task<IActionResult> CreateSession()
        {
            var session = new ChatbotSession
            {
                UserId = GetUserId(),
                UserType = GetUserType(),
                Title = "New Chat",
                ModelName = "gpt"
            };

            _context.ChatbotSessions.Add(session);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                session.SessionId,
                session.Title
            });
        }

      
        [HttpPost("session/{sessionId:int}/message")]
        public async Task<IActionResult> SendMessage(int sessionId, ChatbotSendMessageDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest("Message is required");

            var userId = GetUserId();

            var session = await _context.ChatbotSessions
                .Include(s => s.Messages)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId && s.UserId == userId);

            if (session == null)
                return Forbid();

            bool isFirstMessage = !session.Messages.Any(m => m.Role == "user");

           
            _context.ChatbotMessages.Add(new ChatbotMessage
            {
                SessionId = sessionId,
                Role = "user",
                Content = dto.Message.Trim(),
                AnswerIndex = 0
            });

            if (isFirstMessage)
            {
                session.Title = dto.Message.Length > 40
                    ? dto.Message[..40] + "..."
                    : dto.Message;
            }

            string aiReply1 = session.UserType == "doctor"
                ? "AI medical guideline–based answer (doctor level)."
                : "AI simplified medical answer for patient.";

            string aiReply2 = session.UserType == "doctor"
                ? "Alternative treatment / second opinion."
                : "General advice and when to consult a doctor.";

            _context.ChatbotMessages.AddRange(
                new ChatbotMessage
                {
                    SessionId = sessionId,
                    Role = "assistant",
                    Content = aiReply1,
                    AnswerIndex = 1
                },
                new ChatbotMessage
                {
                    SessionId = sessionId,
                    Role = "assistant",
                    Content = aiReply2,
                    AnswerIndex = 2
                }
            );

            await _context.SaveChangesAsync();

            return Ok(new
            {
                answers = new[]
                {
                    new { index = 1, content = aiReply1 },
                    new { index = 2, content = aiReply2 }
                }
            });
        }

        [HttpGet("session/{sessionId:int}")]
        public async Task<IActionResult> GetSession(int sessionId)
        {
            var userId = GetUserId();

            var session = await _context.ChatbotSessions
                .AsNoTracking()
                .Include(s => s.Messages)
                .FirstOrDefaultAsync(s => s.SessionId == sessionId && s.UserId == userId);

            if (session == null)
                return Forbid();

            return Ok(new ChatbotSessionDto
            {
                SessionId = session.SessionId,
                Title = session.Title,
                UserType = session.UserType,
                Messages = session.Messages
                    .OrderBy(m => m.CreatedAt)
                    .Select(m => new ChatbotMessageDto
                    {
                        Role = m.Role,
                        Content = m.Content,
                        AnswerIndex = m.AnswerIndex,
                        CreatedAt = m.CreatedAt
                    })
                    .ToList()
            });
        }

        
        [HttpGet("sessions")]
        public async Task<IActionResult> GetMySessions()
        {
            var userId = GetUserId();
            var userType = GetUserType();

            var sessions = await _context.ChatbotSessions
                .AsNoTracking()
                .Where(s => s.UserId == userId && s.UserType == userType)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new
                {
                    s.SessionId,
                    s.Title,
                    s.CreatedAt
                })
                .ToListAsync();

            return Ok(sessions);
        }
    }
}
