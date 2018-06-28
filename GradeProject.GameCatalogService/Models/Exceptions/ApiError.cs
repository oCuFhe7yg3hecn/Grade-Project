using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace GradeProject.GameCatalogService.Filters
{
    internal class ApiError
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public ValidationErrorCollection Errors { get; set; }

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
                //errors = modelState.SelectMany(m => m.Value.Errors).ToDictionary(m => m.Key, m=> m.ErrorMessage);
                //errors = modelState.SelectMany(m => m.Value.Errors.Select( me => new KeyValuePair<string,string>( m.Key,me.ErrorMessage) ));
                //errors = modelState.SelectMany(m => m.Value.Errors.Select(me => new ModelError { FieldName = m.Key, ErrorMessage = me.ErrorMessage }));
            }
        }
    }
}