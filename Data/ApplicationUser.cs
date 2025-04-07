using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WoundClinic.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
[PrimaryKey(nameof(Id), nameof(PersonNationalCode))]
public class ApplicationUser : IdentityUser
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long PersonNationalCode { get; set; }

    
    public Person Person { get; set; }

    public ICollection<Patient> Patients { get; set; }

    public ICollection<WoundCare> WoundCares { get; set; }

}

