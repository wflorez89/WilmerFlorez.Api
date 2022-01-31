using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace WilmerFlorez.Models.Input
{
    public class OwnerAddInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public IFormFile Photo { get; set; }

        [Required]
        public DateTime? Birthday { get; set; }
    }
}
