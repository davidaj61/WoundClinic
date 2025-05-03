using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WoundClinic.Data
{
    [PrimaryKey(nameof(NationalCode))]  
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long NationalCode { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(25)")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public bool Gender { get; set; }


        public Patient? Patient { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public string DisplayName => FirstName + " " + LastName;

    }
}
