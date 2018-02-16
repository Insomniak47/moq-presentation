using NantauCommon.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace NantauCommon.Interfaces
{
    public interface ISink<T>
    {
        StorageFormat Format { get; }
        void Save(IEnumerable<T> members);
    }
}
