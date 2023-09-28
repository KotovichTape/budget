using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace budget
{
    internal static class JSONConv
    {
        public static T getInfo<T>(string path)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
        public static void saveInfo<T>(string path, T list)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(list));
        }
    }
}
