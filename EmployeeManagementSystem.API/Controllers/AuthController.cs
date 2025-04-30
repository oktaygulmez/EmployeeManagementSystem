using EmployeeManagementSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Swashbuckle.AspNetCore;
using EmployeeManagementSystem.Data.Entities;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        private static Dictionary<string, (int count, DateTime? lockoutEnd)> loginAttempts = new();
        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Login açıklama metni
        /// </summary>
        [HttpPost("login")]
        public IActionResult Login([FromBody] AdminUser login)
        {
            var username = login.EMail;
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            // Daha önce başarısız deneme var mı kontrolü
            if (loginAttempts.TryGetValue(username, out var attemptInfo))
            {
                if (attemptInfo.lockoutEnd.HasValue && attemptInfo.lockoutEnd > DateTime.UtcNow)
                {
                    var remaining = (attemptInfo.lockoutEnd.Value - DateTime.UtcNow).TotalSeconds;
                    _logger.LogWarning("Kilitli kullanıcı: {User}, IP: {IP}, Kalan süre: {Seconds}s", username, ipAddress, remaining);
                    return BadRequest($"Çok fazla başarısız deneme. Lütfen {Math.Ceiling(remaining)} saniye sonra tekrar deneyin.");
                }
            }

            // Kullanıcı doğrulama
            if (username == "test" && login.Password == "password")
            {
                // Başarılı giriş -> denemeleri temizle
                if (loginAttempts.ContainsKey(username))
                    loginAttempts.Remove(username);

                var claims = new[]
                {
                   new Claim(ClaimTypes.Email, login.EMail)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationInMinutes"])),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            // Başarısız giriş -> deneme sayısını artır
            if (!loginAttempts.ContainsKey(username))
                loginAttempts[username] = (1, null);
            else
            {
                var (count, _) = loginAttempts[username];
                int newCount = count + 1;
                DateTime? lockoutEnd = newCount >= 3 ? DateTime.UtcNow.AddMinutes(1) : null;
                loginAttempts[username] = (newCount, lockoutEnd);

                if (lockoutEnd != null)
                {
                    _logger.LogWarning("Kullanıcı kilitlendi: {User}, IP: {IP}, Saat: {Time}", username, ipAddress, DateTime.UtcNow);
                }
            }

            _logger.LogWarning("Başarısız giriş denemesi: {User}, IP: {IP}, Tarih: {Date}", username, ipAddress, DateTime.UtcNow);

            return Unauthorized("Geçersiz kullanıcı adı veya şifre");
        }

    }
}
