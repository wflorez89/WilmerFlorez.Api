using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using System.Threading.Tasks;
using System.Collections.Generic;
using WilmerFlorez.Common.Transverses;

namespace WilmerFlorez.Logic.Interfaces
{
    public interface IOwnerLogic
    {
        Task<CommonResult<OwnerOutput>> Create(OwnerInput input);
        Task<CommonResult<IEnumerable<OwnerOutput>>> GetAll();
    }
}
