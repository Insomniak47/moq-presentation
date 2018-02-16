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
        public void GetCustomerById_ValidSomewhere_ReturnsCusomer()
        {

            //Arrange:
            _provider.Setup(x => x.Load())
                .Returns(_validCustomers);

            _cache.Setup(x => x.GetCustomerById(_validGuid))
                .Returns(_validCustomer);

            var picker = new CachePicker(_provider.Object, _cache.Object);

            //Act:
            var response = picker.GetCustomerById(_validGuid);

            //Assert:
            Assert.AreEqual(_validGuid, response.Id);
        }

        [TestMethod]
        public void GetCustomerById_InCache_ReturnsFromCache()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetCustomerById_NotInCache_CallsToProvider()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void GetCustomerById_NotInCache_ChecksCache()
        {
            throw new NotImplementedException();
        }


    }
}
