using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using System.Threading.Tasks;
using WilmerFlorez.Common.Transverses;

namespace WilmerFlorez.Logic.Interfaces
{
    public interface IPropertyImageLogic
    {
        Task<CommonResult<PropertyImageOutput>> Add(PropertyImageInput input);
    }
}
