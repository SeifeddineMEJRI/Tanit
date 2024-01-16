using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Tanit.Common.FluentResult.Success;

public class CreatedSuccess<T>: FluentResults.Success, ITanitReason
{
    private readonly T _identifier;

    public CreatedSuccess(T identifier)
    {
        _identifier = identifier;
    }

    public IActionResult ToActionResult()
    {
        return new ContentResult
        {
            StatusCode = (int)HttpStatusCode.Created,
            Content = JsonConvert.SerializeObject(new {Id = _identifier}),
            ContentType = "text/plain"
        };
    }
}