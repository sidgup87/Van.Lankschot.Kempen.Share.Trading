using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Shares.Trading.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Van.Lankschot.Kempen.Api.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        /// <summary>
        ///     ctor
        /// </summary>
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(NotFoundException), HandleNotFoundException }
            };

            _logger = logger;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Handling exception:");

            var type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private static void HandleNotFoundException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = context.Exception.Message,
                Type = "https://localhost:7149/probs/not-found"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            context.ExceptionHandled = true;
        }

        private static void HandleBadRequestException(ExceptionContext context)
        {
            var ex = (context.Exception as BadRequestException);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = ex.Message,
                Detail = ex.Details,
                Type = "https://localhost:7149/probs/bad-request"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }

        private static void HandleUnknownException(ExceptionContext context)
        {
            var ex = context.Exception;

            var detailText = ex.Message;

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                detailText += $" | {ex.Message}";
            }

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Detail = detailText,
                Type = "https://localhost:7149/probs/unkwon-error"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
