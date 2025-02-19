using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistence
{
    public class BlogDbContext: DBContext
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BlogDB;Username=tu_usuario;Password=tu_contraseña");
        //    }
        //}

    }
}