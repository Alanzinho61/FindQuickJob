using FindQuickJob.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(Guid id);
        Task<IEnumerable<UserDto>> GetAllUserAsync();
        Task<UserDto> CreateUser(CreateUserDto createUserDto);

    }
}
