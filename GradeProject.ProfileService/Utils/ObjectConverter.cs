using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Utils
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
    }
}
