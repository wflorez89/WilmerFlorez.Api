using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Implementations;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;

namespace WilmerFlorez.Test.Logic
{
    public class PropertyLogicTest
    {
        private PropertyLogic _propertyLogic;

        private Mock<IPropertyPersistence> _propertyPersistence;
        private Mock<ILogger<OwnerLogic>> _logger;
        private Mock<IImageLogic> _imageLogic;


        [SetUp]
        public void Setup()
        {
            _propertyPersistence = new Mock<IPropertyPersistence>();
            _logger = new Mock<ILogger<OwnerLogic>>();
            _imageLogic = new Mock<IImageLogic>();

            _propertyLogic = new PropertyLogic(
                    _propertyPersistence.Object,
                   _logger.Object,
                   _imageLogic.Object);

            Configure();
        }

        private void Configure() 
        {
            
            _propertyPersistence.Setup(x =>  x.ChangePrice(It.IsAny<ChangePriceInput>()))
                .Returns(Task.FromResult(true));

            var resultCreate = GetCreate();
            _propertyPersistence.Setup(x => x.Create(It.IsAny<PropertyInput>()))
                .Returns(Task.FromResult(resultCreate));

            _propertyPersistence.Setup(x => x.Update(It.IsAny<PropertyInputUpdate>()))
                .Returns(Task.FromResult(resultCreate));

            var result = GetReturnGetAll();
            _propertyPersistence.Setup(x => x.GetAll(It.IsAny<FilterInput>()))
               .Returns(Task.FromResult(result));
        }

        private PropertyOutput GetCreate()
        {
            return new PropertyOutput
            {
                Id = 1,
                IdOwner = 25,
                Name = "Property 1",
                CodeInternatal = "132152",
                Price = 25000,
                Address = "adasd",
                Year = 2022
            };
        }

        private IEnumerable<PropertyOutput> GetReturnGetAll()
        {
            var result = new List<PropertyOutput> {
                  new PropertyOutput
                    {
                        Id = 1,
                        IdOwner = 25,
                        Name = "Property 1",
                        CodeInternatal = "132152",
                        Price = 25000,
                        Address = "adasd",
                        Year = 2022
                  },
                   new PropertyOutput
                    {
                        Id = 1,
                        IdOwner = 25,
                        Name = "Property 1",
                        CodeInternatal = "132152",
                        Price = 25000,
                        Address = "adasd",
                        Year = 2022
                   }
            };

            return result;
        }

        [Test]
        public async Task ChangePriceTest()
        {
            var input = new ChangePriceInput
            {
                IdProperty = 25,
                Price    = 25000
            };

            var result = await _propertyLogic.ChangePrice(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<bool>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(true, result.Result);
        }

        [Test]
        public async Task CreateTest()
        {
            var input = new PropertyInput
            {
                IdOwner = 25,
                Name = "Property 1",
                CodeInternatal = "132152",
                Price = 25000,
                Address = "adasd",
                Year = 2022
            };

            var result = await _propertyLogic.Create(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<PropertyOutput>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(1, result.Result.Id);
        }

        [Test]
        public async Task UpdateTest()
        {
            var input = new PropertyInputUpdate
            {
                IdProperty = 1,
                IdOwner = 25,
                Name = "Property 1",
                CodeInternatal = "132152",
                Price = 25000,
                Address = "adasd",
                Year = 2022
            };

            var result = await _propertyLogic.Update(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<PropertyOutput>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(1, result.Result.Id);
        }

        [Test]
        public async Task GetAllTest()
        {
            var input = new FilterInput {
            PageSize = 20,
            PageNumber = 1
            };
            var result = await _propertyLogic.GetAll(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<IEnumerable<PropertyOutput>>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(2, result.Result.ToList().Count());
        }
    }
}
