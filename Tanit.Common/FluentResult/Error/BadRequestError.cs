using System.Net;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Tanit.Common.FluentResult;

public class BadRequestError : Error, ITanitReason
{
    private readonly IEnumerable<IError> _validationErrors;

    public BadRequestError(IEnumerable<IError> validationErrors)
    {
        this._validationErrors = validationErrors;
    }

    public IActionResult ToActionResult()
    {
        return new ContentResult
        {
            StatusCode = (int)HttpStatusCode.BadRequest,
            Content = JsonConvert.SerializeObject(_validationErrors),
            ContentType = "text/plain"
        };
    }
}