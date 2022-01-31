using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace WilmerFlorez.Models.Input
{
    public class PropertyAddImageInput
    {
        [Required]
        public int? IdProperty { get; set; }

        [Required]
        [JsonIgnore]
        public IFormFile File { get; set; }

    }
}
