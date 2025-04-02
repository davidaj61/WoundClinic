using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WoundClinic.Data
{
    public class Dressing
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [DisplayName("نام پانسمان")]
        public string DressingName { get; set; }

        [DisplayName("قیمت ثابت دارد")]
        public bool HasConstPrice { get; set; }

        [DisplayName("قیمت")]
        public int Price { get; set; } = 0;


        public ICollection<DressingCare> DressingCare { get; set; }
    }
}
