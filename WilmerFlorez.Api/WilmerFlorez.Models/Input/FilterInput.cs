using System.ComponentModel.DataAnnotations;

namespace WilmerFlorez.Models.Input
{
    public class FilterInput
    {
        [Required]
        public int PageSize { get; set; }
        
        [Required]
        public int PageNumber { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }
    }
}
