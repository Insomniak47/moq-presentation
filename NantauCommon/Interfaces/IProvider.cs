using System;
using System.Collections.Generic;

namespace NantauCommon.Interfaces
{
    public interface IProvider<T>
    {
        IEnumerable<T> Load();
        void Save(IEnumerable<T> toSave);
    }
}
