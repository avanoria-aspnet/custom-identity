using Microsoft.EntityFrameworkCore;
using Presentation.WebApp.Data;

namespace Presentation.WebApp.Services;

public class UserService(DataContext context, IPasswordHasher passwordHasher) : IUserService
{
    public async Task<ApplicationUser> CreateUserAsync(string email, string password)
    {
        var exists = await context.Users.AnyAsync(x => x.Email == email);
        if (exists)
            throw new InvalidOperationException("User already exists");


        var hashResult = passwordHasher.HashPassword(password);

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid().ToString(),
            Email = email.Trim(),
            PasswordHash = hashResult.HashedPassword,
            PasswordSalt = hashResult.Salt,
            CreatedAt = DateTime.UtcNow,
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }

    public async Task<ApplicationUser?> ValidateCedentialsAsync(string email, string password)
    {
        var user = await context.Users.SingleOrDefaultAsync(x => x.Email == email);
        if (user == null)
            return null;

        var isValid = passwordHasher.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
        return isValid ? user : null;
    }
}
