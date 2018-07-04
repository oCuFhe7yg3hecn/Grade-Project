using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace GradeProject.MVCWeb.Models
{
    public class ODataResponse<T>
    {
        [DataMember(Name = "@odata.context")]
        public string OdataContext { get; set; }
        public T[] Value { get; set; }
    }
}
