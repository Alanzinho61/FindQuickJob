using AutoMapper;
using FindQuickJob.Application.Dtos;
using FindQuickJob.Domain.Entities;
using FindQuickJob.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            var user = new User
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                PasswordHash = createUserDto.Password,
                PhoneNumber = createUserDto.PhoneNumber
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUserAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            //var userById= await _context.Users.FindAsync(id);
            var userById = await _context.Users.FirstOrDefaultAsync(u=>u.Id==id);
            return _mapper.Map<UserDto>(userById);
        }
    }
}
