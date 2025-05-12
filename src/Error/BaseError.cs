using Microsoft.AspNetCore.Mvc;

namespace ElvenScript.Error;

public abstract record BaseError(string Code, string Description)
{
    public abstract ProblemDetails ToProblemDetails();
}
