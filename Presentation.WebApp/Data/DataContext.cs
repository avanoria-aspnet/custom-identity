using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Presentation.WebApp.Data;

public class DataContext(DbContextOptions<DataContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{

}
