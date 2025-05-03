using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Numerics;
using WoundClinic.Repository;

namespace WoundClinic.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly PersonRepository _person;


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        PersonRepository person)
        : base(options)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _person = person;
    }

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
            .HasForeignKey<Patient>(q=>q.NationalCode)
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
            .HasForeignKey<ApplicationUser>(e=>e.PersonNationalCode)
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

    public async Task<Person> seedPersonAsync()
    {
        Person seedPerson = new Person()
        {
            NationalCode = 1285046358,
            FirstName="داود",
            LastName="اقاویل جهرمی",
            Gender=true
        };

        return await _person.CreateAsync(seedPerson);

    }

    public async Task SeedAdminUserAsync()
    {
        const string adminRoleName = "Administrator";
        const string username = "1285046358";
        const string adminPassword = "Admin@123";
        Person person=await _person.CreateAsync( new Person()
        {
            NationalCode = 1285046358,
            FirstName = "داود",
            LastName = "اقاویل جهرمی",
            Gender = true
        });

        // Create the Administrator role if it doesn't exist
        if (!await _roleManager.RoleExistsAsync(adminRoleName))
        {
            await _roleManager.CreateAsync(new ApplicationRole { Name = adminRoleName,DisplayName="مدیر سیستم",  });
        }

        // Create the Administrator user if it doesn't exist
        var adminUser = await _userManager.FindByNameAsync(username);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = username,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PersonNationalCode =long.Parse(username),
                Person=person
            };

            var result = await _userManager.CreateAsync(adminUser, adminPassword);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(adminUser, adminRoleName);
            }
        }
    }

}
