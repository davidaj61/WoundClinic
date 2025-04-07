using Microsoft.EntityFrameworkCore;
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

        public long  PatientId { get; set; }

        public long UserId { get; set; }

        public DateTime Date { get; set; }

        public string? Description { get; set; }

        [ForeignKey(nameof(PatientId))]
        public Patient Patient { get; set; }

        [ForeignKey(nameof(UserId)),DeleteBehavior(DeleteBehavior.Restrict)]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<DressingCare> DressingCares { get; set; }


    }
}
