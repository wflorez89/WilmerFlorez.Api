using System.ComponentModel.DataAnnotations;

namespace WilmerFlorez.Models.Input
{
    public class PropertyInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [Required]
        public string CodeInternatal { get; set; }
        [Required]
        public int? Year { get; set; }

        [Required]
        public int? IdOwner { get; set; }
    }
}
