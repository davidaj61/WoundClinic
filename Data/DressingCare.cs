using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoundClinic.Data
{
    public class DressingCare
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int WoundCareId { get; set; }

        public byte DressingId { get; set; }

        public byte Quantity { get; set; }

        public int Price { get; set; }

        public int Payment => Quantity * Price;

        [ForeignKey(nameof(WoundCareId))]
        public WoundCare WoundCare { get; set; }

        [ForeignKey(nameof(DressingId))]
        public Dressing Dressing { get; set; }

        
    }
}