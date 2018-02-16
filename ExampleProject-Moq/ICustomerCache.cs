using ExampleProjectMoq;
using System;

namespace ExampleProject_Moq
{
    public interface ICustomerCache
    {
        /// <summary>
        /// Checks if a customer exists in the cache storage
        /// </summary>
        /// <param name="id"> A valid <see cref="Guid"/></param>
        /// <returns> True if the customer exists in the storage, otherwise false</returns>
        bool Exists(Guid id);

        /// <summary>
        /// Retrieves a customer from the cache
        /// </summary>
        /// <param name="id"> A valid </param>
        /// <exception cref="IndexOutOfRangeException"> When the <paramref name="id"/> doesn't exist in the cache </exception>
        /// <returns>A valid <see cref="Customer"/> </returns>
        Customer GetCustomerById(Guid id);
    }
}