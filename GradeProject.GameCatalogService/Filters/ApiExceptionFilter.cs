using GradeProject.GameCatalogService.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Api Exception");

            var apiError = new ApiError();
            if (context.Exception is ApiException)
            {
                var apiExc = context.Exception as ApiException;
                context.Exception = null;
                apiError.Message = apiExc.Message;

                context.HttpContext.Response.StatusCode = apiExc.StatusCode;
            }
            else
            {
                #if !DEBUG
                    var msg = "An unhandled error occurred.";                
                    string stack = null;
                #else
                    var msg = context.Exception.GetBaseException().Message;
                    string stack = context.Exception.StackTrace;
                #endif
                    apiError = new ApiError(msg, stack);

                context.HttpContext.Response.StatusCode = 500;
            }

            context.Result = new ObjectResult(apiError);

            base.OnException(context);
        }
    }
}
