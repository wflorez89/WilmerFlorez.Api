using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using System.Threading.Tasks;

namespace WilmerFlorez.Persistence.Interfaces
{
    public interface IPropertyImagePersistence
    {
        Task<PropertyImageOutput> Add(PropertyImageInput input);
    }
}
