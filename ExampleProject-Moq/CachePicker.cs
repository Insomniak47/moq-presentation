using ExampleProjectMoq;
using NantauCommon.Interfaces;
using System;
using System.Linq;

namespace ExampleProject_Moq
{
    //TODO: CN -- Write this class. 
    //END GOAL: Return something to a customer from a storage location by Id
    //Requirements -> Check a cache for it first
    //             -> Throw if we get a nil Id

   

    public class CachePicker
    {
        private IProvider<Customer> _provider;
        private ICustomerCache _cache;

        public CachePicker(IProvider<Customer> provider, ICustomerCache cache)
        {
            _provider = provider;
            _cache = cache;
        }

        public Customer GetCustomerById(Guid id)
        {
            //TODO:CN -- This ain't good
            return _provider.Load().First(x => x.Id == id);
        }
    }
}
