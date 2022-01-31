using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Implementations;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;

namespace WilmerFlorez.Test.Logic
{
    public class PropertyImageLogicTest
    {
        private PropertyImageLogic _propertyImageLogic;

        private Mock<IPropertyImagePersistence> _propertyImagePersistence;
        private Mock<ILogger<OwnerLogic>> _logger;
        private Mock<IImageLogic> _imageLogic;


        [SetUp]
        public void Setup()
        {
            _propertyImagePersistence = new Mock<IPropertyImagePersistence>();
            _logger = new Mock<ILogger<OwnerLogic>>();
            _imageLogic = new Mock<IImageLogic>();

            _propertyImageLogic = new PropertyImageLogic(
                    _propertyImagePersistence.Object,
                   _logger.Object);

            Configure();
        }

        private void Configure() 
        {
            var output = GetCreate();
            _propertyImagePersistence.Setup(x =>  x.Add(It.IsAny<PropertyImageInput>()))
                .Returns(Task.FromResult(output));

           
        }

        private PropertyImageOutput GetCreate()
        {
            return new PropertyImageOutput
            {
                IdProperty = 1,
                File = "https://www.weelo.com/src/img/Welcome.svg"
            };
        }


        [Test]
        public async Task GetAllTest()
        {
            var input = new PropertyImageInput
            {
               File = "https://www.weelo.com/src/img/Welcome.svg",
               IdProperty = 1
            };
            var result = await _propertyImageLogic.Add(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<PropertyImageOutput>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual("https://www.weelo.com/src/img/Welcome.svg", result.Result.File);
        }
    }
}
