using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            .HasOne(q => q.Person)
            .WithOne(q => q.Patient)
            .HasForeignKey<Person>()
            .OnDelete(DeleteBehavior.NoAction);



        modelBuilder.Entity<Patient>()
            .HasOne(q => q.ApplicationUser)
            .WithMany(q => q.Patients)
            .HasPrincipalKey(q => q.PersonNationalCode)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(e => e.Person)
            .WithOne(e => e.ApplicationUser)
            .HasPrincipalKey<ApplicationUser>(e=>e.PersonNationalCode)
            .HasForeignKey<Person>(e=>e.NationalCode)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<WoundCare>()
            .HasOne(q => q.ApplicationUser)
            .WithMany(q => q.WoundCares)
            .HasPrincipalKey(q => q.PersonNationalCode)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<WoundCare>()
            .HasOne(q => q.Patient)
            .WithMany(q => q.WoundCares)
            .HasForeignKey(q => q.PatientId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();


        modelBuilder.Entity<WoundCare>()
            .HasMany(q=>q.DressingCares)
            .WithOne(q=>q.WoundCare)
            .HasForeignKey(q=>q.WoundCareId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();  

        modelBuilder.Entity<Dressing>()
            .HasMany(q=>q.DressingCares)
            .WithOne(q=>q.Dressing)
            .HasForeignKey(q=>q.DressingId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
            
    }
}
