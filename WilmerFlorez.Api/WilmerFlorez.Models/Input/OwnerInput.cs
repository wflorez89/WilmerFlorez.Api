using System;
using System.ComponentModel.DataAnnotations;

namespace WilmerFlorez.Models.Input
{
    public class OwnerInput
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
