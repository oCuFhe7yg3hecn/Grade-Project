using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.Utils
{
    public static class ObjectConverter
    {
        public static byte[] ObjectToBytes(object obj)
        {
            if (obj != null)
            {
                var stringyfied = JsonConvert.SerializeObject(obj);
                return Encoding.Default.GetBytes(stringyfied);
            }
            else
            {
                throw new Exception("Cannot serialize null");
            }
        }

        public static T ByteToObject<T>(byte[] bytes)
        {
            if (bytes.Count() == 0)
            {
                var stringyfied = Encoding.Default.GetString(bytes);
                return JsonConvert.DeserializeObject<T>(stringyfied); ;
            }
            else { throw new Exception("There no data"); }
        }
    }
}
