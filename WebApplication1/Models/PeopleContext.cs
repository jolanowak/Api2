using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Models
{
    public class PeopleContext : DbContext
    {
        public DbSet<People> People { get; set; }

        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<People>(ConfigurePeople);
        }

        private void ConfigurePeople(EntityTypeBuilder<People> builder)
        {
            builder.ToTable("People");
            builder.HasKey(p => p.Id);
            

        }
    }
}





//{ //baza w pamięci po restarcie sie resetuje
// public class PeopleContext : DbContext
// {
//   public DbSet<People> People { get; set; }

// public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
//   {

// }
//}
//}
//instalacja - Install-Package Microsoft.EntityFrameworkCore.InMemory