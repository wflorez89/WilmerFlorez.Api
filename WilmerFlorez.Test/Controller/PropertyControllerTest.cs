using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilmerFlorez.Api.Controllers;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Implementations;
using WilmerFlorez.Logic.Interfaces;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;

namespace WilmerFlorez.Test.Controller
{
    public class PropertyControllerTest
    {
        private PropertyController _propertyController;

        private Mock<IPropertyLogic> _propertyLogic;
        


        [SetUp]
        public void Setup()
        {
            _propertyLogic = new Mock<IPropertyLogic>();
            

            _propertyController = new PropertyController(
                    _propertyLogic.Object);

            Configure();
        }

        private void Configure() 
        {

            _propertyLogic.Setup(x =>  x.ChangePrice(It.IsAny<ChangePriceInput>()))
                .Returns(Task.FromResult(new CommonResult<bool> { Result= true }));

            var resultCreate = GetCreate();
            _propertyLogic.Setup(x => x.Create(It.IsAny<PropertyInput>()))
                .Returns(Task.FromResult(new CommonResult<PropertyOutput> { Result = resultCreate }));

            _propertyLogic.Setup(x => x.Update(It.IsAny<PropertyInputUpdate>()))
                .Returns(Task.FromResult(new CommonResult<PropertyOutput> { Result = resultCreate }));

            var result = GetReturnGetAll();
            _propertyLogic.Setup(x => x.GetAll(It.IsAny<FilterInput>()))
               .Returns(Task.FromResult(new CommonResult<IEnumerable<PropertyOutput>> { Result = result }));
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

            var result = await _propertyController.ChangePrice(input);
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

            var result = await _propertyController.Create(input);
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

            var result = await _propertyController.Update(input);
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
            var result = await _propertyController.GetAll(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<IEnumerable<PropertyOutput>>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(2, result.Result.ToList().Count());
        }
    }
}
