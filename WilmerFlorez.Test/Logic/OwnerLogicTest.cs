using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WilmerFlorez.Common.Transverses;
using WilmerFlorez.Logic.Implementations;
using WilmerFlorez.Models.Input;
using WilmerFlorez.Models.Output;
using WilmerFlorez.Persistence.Interfaces;

namespace WilmerFlorez.Test.Logic
{
    public class OwnerLogicTest
    {
        private OwnerLogic _ownerLogic;
        private Mock<IOwnerPersistence> _ownerPersistence;
        private Mock<ILogger<OwnerLogic>> _logger;

        [SetUp]
        public void Setup()
        {
            _ownerPersistence = new Mock<IOwnerPersistence>();
            _logger = new Mock<ILogger<OwnerLogic>>();

            _ownerLogic = new OwnerLogic(_ownerPersistence.Object,
                   _logger.Object);

            Configure();
        }

        private void Configure() 
        {
            var resultCreate = GetReturnCreate();
            _ownerPersistence.Setup(x =>  x.Create(It.IsAny<OwnerInput>()))
                .Returns(Task.FromResult(resultCreate));

            var resultGet = GetReturnGetAll();
            _ownerPersistence.Setup(x => x.GetAll())
                .Returns(Task.FromResult(resultGet));

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
            var input = new OwnerInput {
                Address = "CARRERA 25",
                Birthday = new DateTime(2022,1,1),
                Name = "NOMBRE 1",
                Photo = ""
            };
            var result = await _ownerLogic.Create(input);
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<OwnerOutput>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(25, result.Result.IdOwner);
        }

        [Test]
        public async Task GetAllTest()
        {
            var result = await _ownerLogic.GetAll();
            Assert.NotNull(result);
            Assert.IsInstanceOf<CommonResult<IEnumerable<OwnerOutput>>>(result);
            Assert.AreEqual(false, result.Error);
            Assert.AreEqual(2, result.Result.ToList().Count());
        }
    }
}
