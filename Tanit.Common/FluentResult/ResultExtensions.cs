using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Tanit.Common.FluentResult
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult( this Result result)
        {
            if (result.IsFailed)
            {
                return result.Errors.ToActionResult();
            }
            return result.Successes.Any() ? result.Successes.ToActionResult() : new OkResult();
        }
        
        public static IActionResult ToActionResult<T>( this IResult<T> result)
        {
            if (result.IsFailed)
            {
                return result.Errors.ToActionResult();
            }
            return result.Successes.Any() ? result.Successes.ToActionResult() : new OkObjectResult(result.Value);
        }

        private static IActionResult ToActionResult(this IEnumerable<ISuccess> successes)
        {
            var tanitSuccess = successes.OfType<ITanitReason>().FirstOrDefault();
            return tanitSuccess != null
                ? tanitSuccess.ToActionResult()
                : new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Content = JsonConvert.SerializeObject(successes),
                    ContentType = "text/plain"
                };
        }
        
        private static IActionResult ToActionResult(this IEnumerable<IError> errors)
        {
            var tanitError = errors.OfType<ITanitReason>().FirstOrDefault();
            return tanitError != null
                ? tanitError.ToActionResult()
                : new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = JsonConvert.SerializeObject(errors),
                    ContentType = "text/plain"
                };
        }
    }
}
