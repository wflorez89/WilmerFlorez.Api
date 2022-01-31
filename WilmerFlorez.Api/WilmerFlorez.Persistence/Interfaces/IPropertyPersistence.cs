using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WilmerFlorez.Persistence.Interfaces
{
    public interface IPropertyPersistence
    {
        Task<PropertyOutput> Create(PropertyInput input);
        Task<PropertyOutput> Update(PropertyInputUpdate input);
        Task<IEnumerable<PropertyOutput>> GetAll(FilterInput input);
        Task<bool> ChangePrice(ChangePriceInput input);
    }
}
