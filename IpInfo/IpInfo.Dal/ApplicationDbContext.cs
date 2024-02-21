using IpInfo.Dal.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace IpInfo.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            //т.к. миграций не планируется, то используется данный метод
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //добавление интерцептора для dbcontext
            optionsBuilder.AddInterceptors(new DateInterceptor());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //задание конфигураций для entity из всей сборки
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
