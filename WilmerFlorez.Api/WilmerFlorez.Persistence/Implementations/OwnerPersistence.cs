using WilmerFlorez.Common;
using WilmerFlorez.Common.Exceptions;
using WilmerFlorez.Database;
using WilmerFlorez.Entities;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Extensions;
using WilmerFlorez.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WilmerFlorez.Persistence.Implementations
{
    public class OwnerPersistence : IOwnerPersistence
    {
        private readonly ContextDb _context;
        public OwnerPersistence(ContextDb context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OwnerOutput>> GetAll()
        {
            var collection = await _context.Owner
                .ToListAsync();
            var result = collection.MapTo<IEnumerable<OwnerOutput>>();
            return result;
        }


        public async Task<OwnerOutput> Create(OwnerInput input)
        {
            var exists = await _context.Owner.AnyAsync(c => c.Name == input.Name);
            if (exists) throw new CustomException("Owner name already exists");
            var entitie = input.MapTo<Owner>();
            _context.Owner.Add(entitie);
            await _context.SaveChangesAsync();
            var result = entitie.MapTo<OwnerOutput>();
            return result;
        }
    }
}
