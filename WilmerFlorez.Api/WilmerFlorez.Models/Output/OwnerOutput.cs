using System;

namespace WilmerFlorez.Models.Output
{
    public class OwnerOutput
    {
        public int IdOwner { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
