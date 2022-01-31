using System.ComponentModel.DataAnnotations;

namespace WilmerFlorez.Models.Input
{
    public class ChangePriceInput
    {
        [Required]
        public int? IdProperty { get; set; }

        [Required]
        public decimal? Price { get; set; }
    }
}
