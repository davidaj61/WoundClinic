using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;

namespace WoundClinic.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

    
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<WoundCare> WoundCares { get; set; }
    public DbSet<Dressing> Dressings { get; set; }
    public DbSet<DressingCare> DressingCares { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Patient>()
            .HasOne(q => q.ApplicationUser)
            .WithMany(q => q.Patients)
            .HasPrincipalKey(q => q.NationalCode)
            .HasForeignKey(q => q.UserId)
            .IsRequired();
            

    }
}
