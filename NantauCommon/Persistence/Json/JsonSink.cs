using NantauCommon.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NantauCommon.Persistence.Json
{
    public class JsonSink<T> : ISink<T>
    {
        private string _path;

        public StorageFormat Format => StorageFormat.Json;

        public JsonSink(string path)
        {
            _path = path;
        }

        public void Save(IEnumerable<T> members)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(members));
        }
    }
}
