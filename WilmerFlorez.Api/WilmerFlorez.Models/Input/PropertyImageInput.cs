using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WilmerFlorez.Models.Input
{
    public class PropertyImageInput
    {
        public int IdProperty { get; set; }
        public string File { get; set; }
    }
}
