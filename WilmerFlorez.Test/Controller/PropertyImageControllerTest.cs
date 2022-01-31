using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using NUnit.Framework;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using WilmerFlorez.Api.Controllers;
using WilmerFlorez.Api.Extensions;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;

namespace WilmerFlorez.Test.Controller
{
    public class PropertyImageControllerTest
    {
        private PropertyImageController _propertyImageController;


        private Mock<IPropertyImageLogic> _propertyImageLogic;
        private Mock<IImageLogic> _imageLogic;
        private Mock<IWebHostEnvironment> _hostingEnvironment;
        private Mock<IContext> _context;
        private Mock<IFormFile> _fileMock;

        [SetUp]
        public void Setup()
        {


            _propertyImageLogic = new Mock<IPropertyImageLogic>();
            _imageLogic = new Mock<IImageLogic>();
            _hostingEnvironment = new Mock<IWebHostEnvironment>();
            _context = new Mock<IContext>();

        _propertyImageController = new PropertyImageController(
                    _propertyImageLogic.Object,
                   _imageLogic.Object,
                   _hostingEnvironment.Object,
                   _context.Object);

            Configure();
        }

        private void Configure() 
        {
            var output = GetCreate();
            _propertyImageLogic.Setup(x =>  x.Add(It.IsAny<PropertyImageInput>()))
                .Returns(Task.FromResult(output));

            _imageLogic.Setup(c => c.Upload(It.IsAny<IFormFile>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("https://www.weelo.com/src/img/Welcome.svg");

            _fileMock = new Mock<IFormFile>();

            var stream = GetImage(900, 1900);
            _fileMock.Setup(c => c.OpenReadStream()).Returns(stream);
            _fileMock.Setup(c => c.ContentType).Returns("image/jpeg");

        }

        private CommonResult<PropertyImageOutput> GetCreate()
        {
            return new CommonResult<PropertyImageOutput>
            {
                Result = new PropertyImageOutput
                {
                    IdProperty = 1,
                    File = "https://www.weelo.com/src/img/Welcome.svg"
                }
            } ;
        }

        private Stream GetImage(int height, int width)
        {
            var binap = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(binap))
            {
                Rectangle ImageSize = new Rectangle(0, 0, width, height);
                graph.FillRectangle(Brushes.White, ImageSize);
            }


            var stream = new MemoryStream();
            stream.Position = 0;
            binap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            return stream;
        }


        [Test]
        public async Task AddTest()
        {
            var input = new PropertyAddImageInput
            {
               File = _fileMock.Object,
               IdProperty = 1
            };
            var result = await _propertyImageController.Add(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<PropertyImageOutput>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual("https://www.weelo.com/src/img/Welcome.svg", result.Result.File);
        }
    }
}
