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
    public class OwnerLogic : IOwnerLogic
    {
        private readonly IOwnerPersistence _ownerPersistence;
        private readonly ILogger<OwnerLogic> _logger;

        public OwnerLogic(IOwnerPersistence ownerPersistence,
            ILogger<OwnerLogic> logger)
        {
            _logger = logger;
            _ownerPersistence = ownerPersistence;
        }

        public async Task<CommonResult<OwnerOutput>> Create(OwnerInput input)
        {
            try
            {
                var result = await _ownerPersistence.Create(input);
                return new CommonResult<OwnerOutput>
                {
                    Result = result
                };
            }
            catch (CustomException e) 
            {
                return new CommonResult<OwnerOutput>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<OwnerOutput>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }

        public async Task<CommonResult<IEnumerable<OwnerOutput>>> GetAll()
        {
            try
            {
                var result = await _ownerPersistence.GetAll();
                return new CommonResult<IEnumerable<OwnerOutput>>
                {
                    Result = result
                };
            }
            catch (CustomException e)
            {
                return new CommonResult<IEnumerable<OwnerOutput>>
                {
                    Error = true,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new CommonResult<IEnumerable<OwnerOutput>>
                {
                    Error = true,
                    Message = "No fue posible la ejecucion"
                };
            }
        }
    }
}
