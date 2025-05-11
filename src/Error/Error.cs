using Microsoft.AspNetCore.Mvc;

namespace Mannaz.Error;

public record Error(string Code, string Description, ErrorSeverity Severity = ErrorSeverity.Error) : BaseError(Code, Description)
{
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
