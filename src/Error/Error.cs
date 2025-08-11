using Microsoft.AspNetCore.Mvc;

namespace Lukdrasil.Error;

public record Error(string Code, ErrorSeverity Severity, string Description) : BaseError(Code, Description)
{
    public ErrorSeverity Severity { get; init; } = Severity;

    public override ProblemDetails ToProblemDetails()
    {
        return new ProblemDetails
        {
            Title = Code,
            Detail = Description,
            Status = (int)Severity
        };
    }
}
