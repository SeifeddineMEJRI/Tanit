using Microsoft.AspNetCore.Mvc;

namespace Tanit.Common.FluentResult;

internal interface ITanitReason
{
    IActionResult ToActionResult();
}