using Microsoft.AspNetCore.Mvc;

namespace Tanit.Common.FluentResult.Success;

public class AcceptedSuccess : FluentResults.Success, ITanitReason

{
    public IActionResult ToActionResult()
    {
        return new AcceptedResult();
    }
}