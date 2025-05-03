using FindQuickJob.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindQuickJob.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto>RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}
