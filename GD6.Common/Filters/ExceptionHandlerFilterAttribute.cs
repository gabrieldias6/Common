using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public class ExceptionHandlerFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var errorModel = new ErrorResponseModel { Message = "An error has occured." };

            var exception = context.Exception;
            if (exception == null)
            {
                // should never happen
                return;
            }

            errorModel.Message = exception.Message;

            //if (exception is ApplicationException)
            //{
            context.HttpContext.Response.StatusCode = 400;
            // ...
            //}

            // Other exception types you want to handle ...

            context.Result = new ObjectResult(errorModel);
        }
    }
}
