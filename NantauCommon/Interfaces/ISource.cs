using NantauCommon.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantauCommon.Interfaces
{
    public interface ISource<T>
    {
        StorageFormat Format { get; }
        IEnumerable<T> Load();
    }
}
