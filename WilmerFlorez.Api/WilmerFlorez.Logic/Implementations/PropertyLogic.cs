using WilmerFlorez.Common.Exceptions;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WilmerFlorez.Logic.Implementations
{
    public class PropertyLogic : IPropertyLogic
    {
        private readonly IPropertyPersistence _propertyPersistence;
        private readonly ILogger<OwnerLogic> _logger;
        private readonly IImageLogic _imageLogic;

        public PropertyLogic(IPropertyPersistence propertyPersistence,
            ILogger<OwnerLogic> logger,
            IImageLogic imageLogic)
        {
            _logger = logger;
            _propertyPersistence = propertyPersistence;
            _imageLogic = imageLogic;
        }

        public async Task<CommonResult<bool>> ChangePrice(ChangePriceInput input)
        {
            try
            {
                var result = await _propertyPersistence.ChangePrice(input);
                return new CommonResult<bool>
                {
                    Result = result
                };
            }
            catch (CustomException e)
            {
                return new CommonResult<bool>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<bool>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }

        public async Task<CommonResult<PropertyOutput>> Create(PropertyInput input)
        {
            try
            {
                var result = await _propertyPersistence.Create(input);
                return new CommonResult<PropertyOutput>
                {
                    Result = result
                };
            }
            catch (CustomException e)
            {
                return new CommonResult<PropertyOutput>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<PropertyOutput>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }

        public async Task<CommonResult<IEnumerable<PropertyOutput>>> GetAll(FilterInput input)
        {
            try
            {
                var result = await _propertyPersistence.GetAll(input);
                return new CommonResult<IEnumerable<PropertyOutput>>
                {
                    Result = result
                };
            }
            catch (CustomException e)
            {
                return new CommonResult<IEnumerable<PropertyOutput>>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<IEnumerable<PropertyOutput>>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }

        public async Task<CommonResult<PropertyOutput>> Update(PropertyInputUpdate input)
        {
            try
            {
                var result = await _propertyPersistence.Update(input);
                return new CommonResult<PropertyOutput>
                {
                    Result = result
                };
            }
            catch (CustomException e)
            {
                return new CommonResult<PropertyOutput>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<PropertyOutput>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }
    }
}
