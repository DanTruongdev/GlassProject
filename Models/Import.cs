using GlassECommerce.Helper.Validation;
using System.ComponentModel.DataAnnotations;

namespace GlassECommerce.Models
{
    public class Import
    {
        [Key]
        public int ImportId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "The import quantity must be betweem 1 and 1000")]
        public int Quantity { get; set; }
        [Required]
        [CurrentDateValidation]
        public DateTime ImportDate { get; set; }
        //
        public virtual Model Model { get; set; }
        public virtual User User { get; set; }
    }
}
