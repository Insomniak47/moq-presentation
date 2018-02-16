using ExampleProjectMoq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NantauCommon.Interfaces;
using System;
using System.Collections.Generic;

namespace ExampleProject_Moq.Test
{
    [TestClass]
    public class CachePicker_Test
    {
        private readonly Mock<IProvider<Customer>> _provider;
        private readonly Mock<ICustomerCache> _cache;
        private readonly List<Customer> _validCustomers;
        private readonly Guid _validGuid;
        private readonly Customer _validCustomer;

        //CN: Initialized before every test
        public CachePicker_Test()
        {
            //Set up the base mock models that we're going to have to use. 
            _provider = new Mock<IProvider<Customer>>();
            _cache = new Mock<ICustomerCache>();

            //CN: Model objects that I want to use for my tests
            _validGuid = Guid.NewGuid();
            _validCustomer = new Customer(_validGuid);
            _validCustomers = new List<Customer> { _validCustomer, new Customer(Guid.NewGuid()) };
        }

        //NOT A GREAT TEST METHOD. DONT USE THIS IN REAL LIFE. PLZ
        [TestMethod]
        public void GetCustomerById_ValidSomewhere_ReturnsCustomer()
        {

            //Arrange:
            _provider.Setup(x => x.Load())
                .Returns(_validCustomers);

            _cache.Setup(x => x.GetCustomerById(_validGuid))
                .Returns(_validCustomer);

            var picker = new CachePicker(_provider.Object, _cache.Object);

            //Act:
            var result = picker.GetCustomerById(_validGuid);

            //Assert:
            Assert.AreEqual(_validGuid, result.Id);
        }

        [TestMethod]
        public void GetCustomerById_ValidIdInCache_ReturnsFromCacheNoLoad()
        {
            //Arrange
            _cache.Setup(x => x.Exists(_validGuid))
                .Returns(true);

            _cache.Setup(x => x.GetCustomerById(_validGuid))
                .Returns(_validCustomer);

            var underTest = ConstructCachePicker();

            //Act
            var result = underTest.GetCustomerById(_validGuid);

            //Assert
            Assert.AreEqual(_validCustomer, result);
            _provider.Verify(x => x.Load(), Times.Never);
        }

        [TestMethod]
        public void GetCustomerById_ValidIdNotInCache_CallsToProvider()
        {
            //Arrange:
            _cache.Setup(x => x.Exists(It.IsAny<Guid>()))
                .Returns(false);

            _provider.Setup(x => x.Load())
                .Returns(_validCustomers);

            var underTest = ConstructCachePicker();

            //Act:
            var unused = underTest.GetCustomerById(_validGuid);

            //Assert:
            _provider.Verify(x => x.Load(), Times.Once);
        }

        [TestMethod]
        public void GetCustomerById_NotInCache_ChecksCache()
        {
            //Arrange:
            _cache.Setup(x => x.Exists(It.IsAny<Guid>()))
                .Returns(false);

            _provider.Setup(x => x.Load())
                .Returns(_validCustomers);

            var underTest = ConstructCachePicker();

            //Act:
            var result = underTest.GetCustomerById(_validGuid);

            //Assert:
            Assert.AreEqual(_validCustomer, result);
            _cache.Verify(x => x.Exists(_validGuid), Times.Once);
        }



        [TestMethod]
        public void Sequences()
        {
            _cache.SetupSequence(x => x.Exists(It.IsAny<Guid>()))
                .Returns(true)
                .Returns(false)
                .Returns(true);

            var exists = new List<bool>
            {
                _cache.Object.Exists(Guid.NewGuid()),
                _cache.Object.Exists(Guid.NewGuid()),
                _cache.Object.Exists(Guid.NewGuid())
            };

            CollectionAssert.AreEquivalent(exists, new List<bool> { true, false, true });
        }

        [TestMethod]
        public void OutParams()
        {
            var toReturn = true;
            var mock = new Mock<IOutParamTestClass>();

            mock.Setup(x => x.DoThing(out toReturn));
            mock.Object.DoThing(out var result);

            Assert.AreEqual(toReturn, result);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Exceptions()
        {
            _cache.Setup(x => x.Exists(It.IsAny<Guid>()))
                .Throws(new InvalidOperationException());

            _cache.Object.Exists(_validGuid);
        }

        [TestMethod]
        public void Callbacks()
        {
            var setMe = 0;

            _cache.Setup(x => x.Exists(It.IsAny<Guid>()))
                .Callback((Guid x) => setMe = x.ToString().Length)
                .Returns(true);

            _cache.Object.Exists(_validGuid);

            Assert.AreEqual(Guid.NewGuid().ToString().Length, setMe);
        }


        //CN: Witchcraft.
        [TestMethod]
        public void CallbacksToChangeOutParamCapture()
        {
            var outParamTester = new Mock<IOutParamTestClass>();
            var first = true;
            var second = false;


            outParamTester.Setup(x => x.DoThing(out first))
                .Callback(() => outParamTester.Setup(x => x.DoThing(out second)));

            // ReSharper disable once RedundantAssignment
            var firstResult = false;

            // ReSharper disable once RedundantAssignment
            var secondResult = true;

            outParamTester.Object.DoThing(out firstResult);
            outParamTester.Object.DoThing(out secondResult);

            Assert.AreEqual(first,firstResult);
            Assert.AreEqual(second,secondResult);

        }

        //CN: More @ https://github.com/Moq/moq4/wiki/Quickstart


        private CachePicker ConstructCachePicker()
        {
            //CN: For convenience
            return new CachePicker(_provider.Object, _cache.Object);
        }


        public interface IOutParamTestClass
        {
            void DoThing(out bool thing);
        }
    }
}
