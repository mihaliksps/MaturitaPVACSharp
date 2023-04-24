using Microsoft.EntityFrameworkCore;
using MaturitaPVACSharp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MaturitaPVACSharp.Data
{
    public class MaturitaPVACSharpContext : DbContext
    {
        public MaturitaPVACSharpContext(DbContextOptions<MaturitaPVACSharpContext> options) : base(options) { }

        public DbSet<Uzivatel> Uzivatele { get; set; }
        public DbSet<Clanek> Clanky { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Clanek>()
                .HasOne(c => c.Autor)
                .WithMany(a => a.Clanky);
        }
    }
}
