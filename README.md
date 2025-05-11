# ElvenScript.Error

ElvenScript.Error is a lightweight library for handling and representing errors in .NET applications. It provides a structured way to define, manage, and convert errors into HTTP problem details, making it ideal for use in ASP.NET Core applications.

## Features

- **Error Representation**: Define errors with a code, description, and severity.
- **Problem Details Conversion**: Easily convert errors into `ProblemDetails` for standardized HTTP responses.
- **Enum-Based Severity**: Categorize errors using `ErrorSeverity` (Error, Warning, Info).
- **BaseError Abstraction**: Extendable base class for custom error types.

## Requirements

- **.NET Version**: .NET 8.0
- **Framework Reference**: Microsoft.AspNetCore.App
