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
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public bool Gender { get; set; }

        [ForeignKey(nameof(NationalCode))]
        public Patient? Patient { get; set; }

        [ForeignKey(nameof(NationalCode))]
        public ApplicationUser ApplicationUser { get; set; }

    }
}
