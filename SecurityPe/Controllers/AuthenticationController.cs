using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SecurityPe.Domain;
using SecurityPe.Models;
using SecurityPe.Services;
using SecurityPe.Settings;
using System.Text.Json;
using SecurityPe.Data;


namespace SecurityPe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IOptions<TokenSettings> _tokenSettings;
        private readonly SqlUserKeyData _keyData;
        private ChatAppContext _context;

        public AuthenticationController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IPasswordHasher<User> passwordHasher,
            IOptions<TokenSettings> tokenSettings, SqlUserKeyData keyData, ChatAppContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordHasher = passwordHasher;
            _tokenSettings = tokenSettings;
            _keyData = keyData;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            //The next line can be commented out because of the [ApiController] attribute
            //if (!ModelState.IsValid) return BadRequest(ModelState);
            //TODO (someday): add captcha validation to prevent registration by bots
           
            var hashPassword = _passwordHasher.HashPassword(null, model.Password);
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                string role =Role.User;
                await EnsureRoleExists(role);
                await _userManager.AddToRoleAsync(user, role);
                AddKeysToDb(model);
                return Ok();
            }

            //put the errors that Identity reported into the response
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        private void AddKeysToDb(RegisterModel model)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            var encryptionServices = new EncryptionServices();
            var aesIv = EncryptionServices.GetAesIV();
            user.AesIv = aesIv;
            user.PrivateKey = encryptionServices.GetPrivateKey();
            _context.PublicKeyStores.Add(new PublicKeyStore
            {
                Email = model.Email,
                PublicKey = encryptionServices.GetPublicKey()
            });
            _context.SaveChanges();
        }


        [HttpPost("token")]
        public async Task<IActionResult> CreateToken([FromBody] LoginModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            if (_passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) !=
                PasswordVerificationResult.Success)
            {
                return Unauthorized();
            }

            string token = await CreateJwtToken(user);
            return Ok(token);
        }

        private async Task EnsureRoleExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new Role
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpper()
                });
            }
        }

        private async Task<string> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var allClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            }.Union(userClaims).ToList();

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                var roleClaim = new Claim(ClaimTypes.Role, role);
                allClaims.Add(roleClaim);
            }

            var keyBytes =Encoding.ASCII.GetBytes(_tokenSettings.Value.Key);
            var symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenSettings.Value.Issuer,
                audience: _tokenSettings.Value.Audience,
                claims: allClaims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.Value.ExpirationTimeInMinutes),
                signingCredentials: signingCredentials);

            string encryptedToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return encryptedToken;
        }
    }
}