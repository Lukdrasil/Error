using Microsoft.AspNetCore.Mvc;

namespace Lukdrasil.Error;

public abstract record BaseError(string Code, string Description)
{
    public abstract ProblemDetails ToProblemDetails();
}
