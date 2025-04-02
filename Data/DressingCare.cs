using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoundClinic.Data
{
    public class DressingCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(WoundCare))]
        public int WoundCareId { get; set; }

        [ForeignKey(nameof(Dressing))]
        public byte DressingId { get; set; }

        public byte Quantity { get; set; }

        public int Price { get; set; }

        public int Payment => Quantity * Price;

        public WoundCare WoundCare { get; set; }

        public Dressing Dressing { get; set; }

        
    }
}