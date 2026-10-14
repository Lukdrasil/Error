using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Lukdrasil.Error;

public record Error(string Code, ErrorSeverity Severity, string Description) : BaseError(Code, Description)
{
    public ErrorSeverity Severity { get; init; } = Severity;

    public override ProblemDetails ToProblemDetails()
    {
        var status = ToHttpStatus(Severity);

        return new ProblemDetails
        {
            Title = string.IsNullOrWhiteSpace(Code) ? ReasonPhrases.GetReasonPhrase(status) : Code,
            Detail = Description,
            Status = status
        };
    }

    private static int ToHttpStatus(ErrorSeverity severity)
    {
        return severity switch
        {
            ErrorSeverity.Error => StatusCodes.Status500InternalServerError,
            ErrorSeverity.Warning => StatusCodes.Status400BadRequest,
            ErrorSeverity.Info => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
