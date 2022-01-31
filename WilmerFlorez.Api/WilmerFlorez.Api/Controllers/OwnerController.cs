using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WilmerFlorez.Api.Extensions;
using WilmerFlorez.Persistence.Extensions;

namespace WilmerFlorez.Api.Controllers
{
    [ApiController]
    [Route("owner")]
    public class OwnerController : ControllerBase
    {

        private readonly IOwnerLogic _ownerLogic;
        private readonly IImageLogic _imageLogic;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IContext _context;

        public OwnerController(IOwnerLogic ownerLogic, IImageLogic imageLogic,
            IWebHostEnvironment hostingEnvironment,
            IContext context)
        {
            _ownerLogic = ownerLogic;
            _imageLogic = imageLogic;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [HttpGet("all")]
        public async Task<CommonResult<IEnumerable<OwnerOutput>>> GetAll()
        {
            return await _ownerLogic.GetAll();
        }

        [HttpPut()]
        public async Task<CommonResult<OwnerOutput>> Create([FromBody] OwnerAddInput input)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var host = _context.GetAbsoluteUrl();
            var image = _imageLogic.Upload(input.Photo, webRootPath, host);
            input.Photo = null;
            var inputOwner = input.MapTo<OwnerInput>();
            inputOwner.Photo = image;
            return await _ownerLogic.Create(inputOwner);
        }

    }
}
