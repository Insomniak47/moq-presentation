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
    public class JsonSource<T> : ISource<T>
    {
        private string _path;

        public JsonSource(string path)
        {
            _path = path;
        }

        public StorageFormat Format => StorageFormat.Json;

        public IEnumerable<T> Load()
        {
            if (File.Exists(_path))
                return JsonConvert.DeserializeObject<IEnumerable<T>>(File.ReadAllText(_path)) ?? new List<T>();
            else
                return new List<T>();
        }
    }
}
