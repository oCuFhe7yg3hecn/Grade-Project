using GradeProject.GameCatalogService.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Models.Exceptions
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public ApiException()
        {

        }

        public ApiException(string message,
                    int statusCode = 500
        ) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
