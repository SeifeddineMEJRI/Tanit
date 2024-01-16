using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Tanit.Common.FluentResult;

public class NotFoundError : Error, ITanitReason
{
    public IActionResult ToActionResult()
    {
        return new NotFoundResult();
    }
}