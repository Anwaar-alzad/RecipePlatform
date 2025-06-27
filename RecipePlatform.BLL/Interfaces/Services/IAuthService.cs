using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipePlatform.BLL.DTOs;

namespace RecipePlatform.BLL.Interfaces.Services
{
    public interface IAuthService
    {
        //يرجع توكين ك سترينج 
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

    }
}
