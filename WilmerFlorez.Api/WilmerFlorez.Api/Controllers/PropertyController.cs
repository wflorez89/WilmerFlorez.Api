using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WilmerFlorez.Api.Controllers
{
    [ApiController]
    [Route("property")]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyLogic _propertyLogic;
        public PropertyController(IPropertyLogic propertyLogic)
        {
            _propertyLogic = propertyLogic;
        }

        [HttpGet("all")]
        public async Task<CommonResult<IEnumerable<PropertyOutput>>> GetAll([FromQuery]FilterInput input)
        {
            return await _propertyLogic.GetAll(input);
        }

        [HttpPut]
        public async Task<CommonResult<PropertyOutput>> Create([FromBody] PropertyInput  input)
        {
            return await _propertyLogic.Create(input);
        }

        [HttpPost]
        public async Task<CommonResult<PropertyOutput>> Update([FromBody] PropertyInputUpdate input)
        {
            return await _propertyLogic.Update(input);
        }

        [HttpPost("change-price")]
        public async Task<CommonResult<bool>> ChangePrice([FromBody] ChangePriceInput input)
        {
            return await _propertyLogic.ChangePrice(input);
        }
    }
}
