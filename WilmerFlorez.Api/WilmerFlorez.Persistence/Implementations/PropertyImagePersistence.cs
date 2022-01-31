using WilmerFlorez.Database;
using WilmerFlorez.Entities;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Extensions;
using WilmerFlorez.Persistence.Interfaces;
using System.Threading.Tasks;

namespace WilmerFlorez.Persistence.Implementations
{
    public class PropertyImagePersistence : IPropertyImagePersistence
    {
        private readonly ContextDb _context;
        public PropertyImagePersistence(ContextDb context)
        {
            _context = context;
        }

        public async Task<PropertyImageOutput> Add(PropertyImageInput input)
        {
            var entitie = input.MapTo<PropertyImage>();
            _context.PropertyImage.Add(entitie);
            await _context.SaveChangesAsync();
            var result = entitie.MapTo<PropertyImageOutput>();
            return result;
        }
    }
}
