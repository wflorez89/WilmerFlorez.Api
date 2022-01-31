using WilmerFlorez.Common;
using WilmerFlorez.Common.Exceptions;
using WilmerFlorez.Database;
using WilmerFlorez.Entities;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Extensions;
using WilmerFlorez.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WilmerFlorez.Persistence.Implementations
{
    public class PropertyPersistence: IPropertyPersistence
    {
        private readonly ContextDb _context;
        public PropertyPersistence(ContextDb context)
        {
            _context = context;
        }

        public async Task<bool> ChangePrice(ChangePriceInput input)
        {
            var entitie = _context.Property.FirstOrDefault(c => c.IdProperty == input.IdProperty);
            if (entitie == null) throw new CustomException("Property doesn't found");
            entitie.Price = input.Price ?? 0;
            return Convert.ToBoolean(await _context.SaveChangesAsync());
        }

        public async Task<PropertyOutput> Create(PropertyInput input)
        {
            var exists = await _context.Property.AnyAsync(c => c.Name == input.Name);
            if (exists) throw new CustomException("Property name already exists");
            exists = await _context.Owner.AnyAsync(c => c.IdOwner == input.IdOwner);
            if (!exists) throw new CustomException("Owner doesn't exists");
            var entitie = input.MapTo<Property>();
            _context.Property.Add(entitie);
            await _context.SaveChangesAsync();
            var result = entitie.MapTo<PropertyOutput>();
            return result;
        }

        public async Task<IEnumerable<PropertyOutput>> GetAll(FilterInput input)
        {
            var collection = await _context.Property
                .WhereIf(!string.IsNullOrEmpty(input.Name), c => c.Name.Contains(input.Name))
                .WhereIf(input.Year != null, c => c.Year == input.Year)
                .Skip(input.PageSize * (input.PageNumber - 1))
                .Take(input.PageSize)
                .ToListAsync();
            var result = collection.MapTo<IEnumerable<PropertyOutput>>();
            return result;
        }

        public async Task<PropertyOutput> Update(PropertyInputUpdate input)
        {
            var entitie = _context.Property.FirstOrDefault(c => c.IdProperty == input.IdProperty);
            if (entitie == null) throw new CustomException("Property doesn't found");
            entitie.Name = input.Name;
            entitie.Address = input.Address;
            entitie.Price = input.Price ?? 0;
            entitie.Year = input.Year ?? 0;
            entitie.CodeInternatal = input.CodeInternatal;
            entitie.IdOwner = input.IdOwner ?? 0;
            await _context.SaveChangesAsync();
            var result = entitie.MapTo<PropertyOutput>();
            return result;
        }

    }
}
