using WilmerFlorez.Common.Exceptions;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace WilmerFlorez.Logic.Implementations
{
    public class PropertyImageLogic : IPropertyImageLogic
    {
        private readonly IPropertyImagePersistence _propertyImagePersistence;
        private readonly ILogger<OwnerLogic> _logger;

        public PropertyImageLogic(IPropertyImagePersistence propertyImagePersistence,
            ILogger<OwnerLogic> logger)
        {
            _logger = logger;
            _propertyImagePersistence = propertyImagePersistence;
        }

        public async Task<CommonResult<PropertyImageOutput>> Add(PropertyImageInput input)
        {
            try
            {
                var result = await _propertyImagePersistence.Add(input);
                return new CommonResult<PropertyImageOutput>
                {
                    Result = result
                };
            }
            catch (CustomException e)
            {
                return new CommonResult<PropertyImageOutput>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<PropertyImageOutput>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }
    }
}
