using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WilmerFlorez.Persistence.Interfaces
{
    public interface IOwnerPersistence
    {
        Task<OwnerOutput> Create(OwnerInput input);
        Task<IEnumerable<  OwnerOutput>> GetAll();
    }
}
