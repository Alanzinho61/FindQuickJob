using AutoMapper;
using BCrypt.Net;
using FindQuickJob.Application.Dtos;
using FindQuickJob.Domain.Entities;
using FindQuickJob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                throw new Exception("User not found");
            return GenerateToken(user);
        }
        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _context.Users.AnyAsync(x => x.Email == registerDto.Email);
            if(existingUser) throw new Exception("User already exists");
            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                PhoneNumber = registerDto.PhoneNumber,
            };
            _context.Add(user);
            _context.SaveChanges();
            return GenerateToken(user);
        }

        private AuthResponseDto GenerateToken(User user) // Bunu Ayri bir classda yapabilirsin
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );
            return new AuthResponseDto
            {
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserId = user.Id
            };

        }
    }
    
}
