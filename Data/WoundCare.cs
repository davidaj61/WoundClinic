using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace WoundClinic.Data
{
    public class WoundCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Patient))]
        public BigInteger  PatientId { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public Patient Patient { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<DressingCare> DressingCares { get; set; }

    }
}
