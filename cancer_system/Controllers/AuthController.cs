using cancer_system.Data;
using cancer_system.DTOs;
using cancer_system.DTOs.PasswordDTO;
using cancer_system.Models.Entities;
using cancer_system.Models.Identity;
using cancer_system.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace cancer_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;


        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ApplicationDbContext context,
            TokenService tokenService,
             EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
           
            if (string.IsNullOrWhiteSpace(dto.FullName))
                return BadRequest(new { message = "Full name is required" });

            if (string.IsNullOrWhiteSpace(dto.Email))
                return BadRequest(new { message = "Email is required" });

            if (string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { message = "Password is required" });

            if (dto.Password != dto.ConfirmPassword)
                return BadRequest(new { message = "Passwords do not match" });

            if (dto.Role != "Patient" && dto.Role != "Doctor")
                return BadRequest(new { message = "Invalid role" });

            if (dto.Role == "Patient" && string.IsNullOrWhiteSpace(dto.CancerType))
                return BadRequest(new { message = "Cancer type is required for patients" });

            if (dto.Role == "Doctor" && dto.EmployeeNumber == null)
                return BadRequest(new { message = "Employee number is required for doctors" });

            
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest(new { message = "Email already exists" });

            
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FullName = dto.FullName
                };

                var createUserResult = await _userManager.CreateAsync(user, dto.Password);
                if (!createUserResult.Succeeded)
                    return BadRequest(createUserResult.Errors);

                var roleResult = await _userManager.AddToRoleAsync(user, dto.Role);
                if (!roleResult.Succeeded)
                {
                    await _userManager.DeleteAsync(user);
                    return BadRequest(roleResult.Errors);
                }

                if (dto.Role == "Patient")
                {
                    _context.Patients.Add(new Patient
                    {
                        FullName = dto.FullName,
                        AspNetUserId = user.Id,
                        CancerType = dto.CancerType!,
                        Gender = "Unknown"

                    });
                }
                else 
                {
                    _context.Doctors.Add(new Doctor
                    {
                        FullName = dto.FullName,
                        AspNetUserId = user.Id,
                        EmployeeNumber = dto.EmployeeNumber!.Value,
                        Specialization = "Unknown"
                    });
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                return BadRequest(new
                {
                    message = "Registration failed",
                    error = ex.InnerException?.Message ?? ex.Message
                });
            }
        }



        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password");

            var result = await _signInManager.CheckPasswordSignInAsync(
                user, dto.Password, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid email or password");

            var role = (await _userManager.GetRolesAsync(user)).First();

            var token = _tokenService.GenerateToken(
                user.Id,
                user.Email!,
                role
            );

            Response.Headers.Append("Authorization", $"Bearer {token}");
            Response.Headers.Append("Access-Control-Expose-Headers", "Authorization");

            return Ok(new { role });
        }

       
        [Authorize]
        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId!);

            if (user == null)
                return Unauthorized();

            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var result = await _userManager.ChangePasswordAsync(
                user,
                dto.CurrentPassword,
                dto.NewPassword
            );

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password changed successfully");
        }

       
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Ok(); 

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var resetLink = $"https://yourapp.com/reset-password?email={dto.Email}&token={Uri.EscapeDataString(token)}";

            await _emailService.SendEmailAsync(
                dto.Email,
                "Reset Your Password",
                $@"
        <h3>Password Reset</h3>
        <p>Click the link below to reset your password:</p>
        <a href='{resetLink}'>Reset Password</a>
        <br/><br/>
        <small>If you didn’t request this, ignore this email.</small>
        "
            );

            return Ok(new { message = "Reset link sent to email" });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (dto.NewPassword != dto.ConfirmPassword)
                return BadRequest("Passwords do not match");

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return BadRequest("Invalid request");

            var result = await _userManager.ResetPasswordAsync(
                user,
                dto.Token,
                dto.NewPassword
            );

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("Password reset successfully");
        }

    }
}
