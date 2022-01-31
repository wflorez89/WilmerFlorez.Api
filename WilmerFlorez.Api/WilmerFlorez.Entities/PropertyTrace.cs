using System;

namespace WilmerFlorez.Entities
{
    public class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }
        public DateTime Datesale { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
        public Property Property { get; set; }
    }
}
