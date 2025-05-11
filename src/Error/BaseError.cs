using Microsoft.AspNetCore.Mvc;

namespace Mannaz.Error;

public abstract record BaseError(string Code, string Description)
{
    public abstract ProblemDetails ToProblemDetails();
}
