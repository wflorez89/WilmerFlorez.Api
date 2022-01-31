using System.Collections.Generic;

namespace WilmerFlorez.Entities
{
    public class Property
    {
        public int IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternatal { get; set; }
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public ICollection<PropertyTrace> PropertyTraces { get; set; }
        public ICollection<PropertyImage> PropertyImages { get; set; }
        public Owner Owner { get; set; }

    }
}
