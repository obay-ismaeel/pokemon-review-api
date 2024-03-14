using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace PokemonReviewApp.Filters;

public class LogSensitiveActionAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Debug.WriteLine(
            $"The action {context.ActionDescriptor.DisplayName} was excuted!",
            LogLevel.Warning);
    }
}
