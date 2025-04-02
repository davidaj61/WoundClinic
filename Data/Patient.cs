using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System;

namespace WoundClinic.Data
{
    public class Patient
    {
        [Key]
        public BigInteger NationalCode { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public bool Gender { get; set; }

        [Required]
        public BigInteger MobileNumber { get; set; }


        public string? Address { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<WoundCare> WoundCares { get; set; }

        
    }

   
}
