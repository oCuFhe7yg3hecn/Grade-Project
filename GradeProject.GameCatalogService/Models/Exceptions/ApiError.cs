using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;

namespace GradeProject.GameCatalogService.Filters
{
    internal class ApiError : Exception
    {
        public string Message { get; set; }
        public string Details { get; set; }

        public ApiError()
        {

        }

        public ApiError(string message)
        {
            Message = message;
        }

        public ApiError(string message, string details)
        {
            Message = message;
            Details = details;
        }

        public ApiError(ModelStateDictionary modelState)
        {
            if (modelState != null && modelState.Any(m => m.Value.Errors.Count > 0))
            {
                Message = "Please correct the specified errors and try again.";
            }
        }
    }
}