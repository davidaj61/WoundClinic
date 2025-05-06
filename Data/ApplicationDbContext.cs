using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;
using WoundClinic.Models;
using WoundClinic.Services;

namespace WoundClinic.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Person> Persons { get; set; }
    public DbSet<WoundCare> WoundCares { get; set; }
    public DbSet<Dressing> Dressings { get; set; }
    public DbSet<DressingCare> DressingCares { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Person>()
            .HasOne(q => q.Patient)
            .WithOne(q => q.Person)
            .HasForeignKey<Patient>(q => q.NationalCode)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);



        modelBuilder.Entity<Patient>()
            .HasOne(q => q.ApplicationUser)
            .WithMany(q => q.Patients)
            .HasPrincipalKey(q => q.PersonNationalCode)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<Person>()
            .HasOne(e => e.ApplicationUser)
            .WithOne(e => e.Person)
            .HasForeignKey<ApplicationUser>(e => e.PersonNationalCode)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

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
            .HasMany(q => q.DressingCares)
            .WithOne(q => q.WoundCare)
            .HasForeignKey(q => q.WoundCareId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        modelBuilder.Entity<Dressing>()
            .HasMany(q => q.DressingCares)
            .WithOne(q => q.Dressing)
            .HasForeignKey(q => q.DressingId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

    }


}


