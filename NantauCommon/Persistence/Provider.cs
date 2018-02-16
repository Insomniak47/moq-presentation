using NantauCommon.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NantauCommon.Persistence
{
    public class Provider<T> : IProvider<T>
    {
        private ISink<T> _sink;
        private ISource<T> _source;

        public Provider(ISink<T> sink, ISource<T> source)
        {
            if (sink.Format != source.Format)
                throw new ArgumentException($"{nameof(sink)} and {nameof(source)} format mismatch in {nameof(Provider<T>)}");
            
            _sink = sink;
            _source = source;
        }

        public IEnumerable<T> Load() => _source.Load();

        public void Save(IEnumerable<T> toSave) => _sink.Save(toSave);
    }
}
