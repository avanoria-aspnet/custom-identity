using Presentation.WebApp.Data;

namespace Presentation.WebApp.Services;

public interface IUserService
{
    Task<ApplicationUser> CreateUserAsync(string email, string password);
    Task<ApplicationUser?> ValidateCedentialsAsync(string email, string password);
}
