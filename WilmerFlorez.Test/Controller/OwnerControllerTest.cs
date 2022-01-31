using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WilmerFlorez.Api.Controllers;
using WilmerFlorez.Api.Extensions;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Implementations;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;

namespace WilmerFlorez.Test.Controller
{
    public class OwnerControllerTest
    {
        private OwnerController _ownerController;

        private Mock<IOwnerLogic> _ownerLogic;
        private Mock<IImageLogic> _imageLogic;
        private Mock<IWebHostEnvironment> _hostingEnvironment;
        private Mock<IContext> _context;
        private Mock<IFormFile> _fileMock;


        [SetUp]
        public void Setup()
        {
             _ownerLogic = new Mock<IOwnerLogic>();
            _imageLogic = new Mock<IImageLogic>();
            _hostingEnvironment = new Mock<IWebHostEnvironment>();
            _context = new Mock<IContext>();
            _fileMock = new Mock<IFormFile>();

        _ownerController = new OwnerController(
            _ownerLogic.Object,
            _imageLogic.Object,
            _hostingEnvironment.Object,
            _context.Object
            );

            Configure();
        }

        private void Configure() 
        {
            var resultCreate = GetReturnCreate();
            _ownerLogic.Setup(x =>  x.Create(It.IsAny<OwnerInput>()))
                .Returns(Task.FromResult(new CommonResult<OwnerOutput> {Result  = resultCreate } ));

            var resultGet = GetReturnGetAll();
            _ownerLogic.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new CommonResult<IEnumerable<OwnerOutput>> { Result = resultGet } ));

            _fileMock = new Mock<IFormFile>();

            var stream = GetImage(900, 1900);
            _fileMock.Setup(c => c.OpenReadStream()).Returns(stream);

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

        private OwnerOutput GetReturnCreate() 
        {
            return new OwnerOutput
            {
                IdOwner = 25,
                Address = "CARRERA 25",
                Birthday = new DateTime(2022, 1, 1),
                Name = "NOMBRE 1",
                Photo = ""
            };
        }

        private IEnumerable<OwnerOutput> GetReturnGetAll()
        {
            var result = new List<OwnerOutput> {
                 new OwnerOutput
                {
                    IdOwner = 1,
                    Address = "CARRERA 1",
                    Birthday = new DateTime(2022, 1, 1),
                    Name = "NOMBRE 1",
                    Photo = ""
                },
                  new OwnerOutput
                {
                    IdOwner = 2,
                    Address = "CARRERA 2",
                    Birthday = new DateTime(2022, 1, 1),
                    Name = "NOMBRE 2",
                    Photo = ""
                },

            };

            return result;
        }

        [Test]
        public async Task CreateTest()
        {
            var input = new OwnerAddInput
            {
                Address = "CARRERA 25",
                Birthday = new DateTime(2022,1,1),
                Name = "NOMBRE 1",
                Photo = _fileMock.Object
            };
            var result = await _ownerController.Create(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<OwnerOutput>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(25, result.Result.IdOwner);
        }

        [Test]
        public async Task GetAllTest()
        {
            var result = await _ownerController.GetAll();
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<IEnumerable<OwnerOutput>>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(2, result.Result.ToList().Count());
        }
    }
}
