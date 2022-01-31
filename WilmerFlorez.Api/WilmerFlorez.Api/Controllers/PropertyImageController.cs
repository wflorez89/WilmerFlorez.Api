using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using WilmerFlorez.Api.Extensions;
using WilmerFlorez.Persistence.Extensions;

namespace WilmerFlorez.Api.Controllers
{
    [ApiController]
    [Route("property-image")]
    public class PropertyImageController : ControllerBase
    {

        private readonly IPropertyImageLogic _propertyImageLogic;
        private readonly IImageLogic _imageLogic;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IContext _context;

        public PropertyImageController(IPropertyImageLogic propertyImageLogic, 
            IImageLogic imageLogic,
            IWebHostEnvironment hostingEnvironment,
            IContext context)
        {
            _propertyImageLogic = propertyImageLogic;
            _imageLogic = imageLogic;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

     
        [HttpPut()]
        public async Task<CommonResult<PropertyImageOutput>> Add([FromBody] PropertyAddImageInput input)
        {
            var webRootPath = _hostingEnvironment.WebRootPath;
            var host = _context.GetAbsoluteUrl();
            var image = _imageLogic.Upload(input.File, webRootPath, host);
            input.File = null;
            var inputProperty = input.MapTo<PropertyImageInput>();
            inputProperty.File = image;
            return await _propertyImageLogic.Add(inputProperty);
        }

    }
}
