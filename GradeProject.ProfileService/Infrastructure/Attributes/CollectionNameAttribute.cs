using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ProfileService.Infrastructure.Attributes
{
    public class CollectionNameAttribute : Attribute
    {
        public string Name { get; set; }

        public CollectionNameAttribute(string name)
        {
            Name = name;
        }
    }
}
