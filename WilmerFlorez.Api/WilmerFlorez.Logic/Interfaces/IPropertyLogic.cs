using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using System.Threading.Tasks;
using System.Collections.Generic;
using WilmerFlorez.Common.Transverses;

namespace WilmerFlorez.Logic.Interfaces
{
    public interface IPropertyLogic
    {
        Task<CommonResult<PropertyOutput>> Create(PropertyInput input);
        Task<CommonResult<PropertyOutput>> Update(PropertyInputUpdate input);
        Task<CommonResult<IEnumerable<PropertyOutput>>> GetAll(FilterInput input);
        Task<CommonResult<bool>> ChangePrice(ChangePriceInput input);
    }
}
