# Error

Error is an .NET  library for structured error and result management. 
It provides a consistent, extensible, and type-safe way to represent, build, and propagate errors in your applications and APIs.

---

## Table of Contents
- [Features](#features)
- [Installation](#installation)
- [Usage](#usage)
  - [Basic Error Creation](#basic-error-creation)
  - [Error with Formatting](#error-with-formatting)
  - [Using ErrorBuilder (Fluent API)](#using-errorbuilder-fluent-api)
  - [Using ErrorBuilder with Direct Text](#using-errorbuilder-with-direct-text)
  - [Exception Safety](#exception-safety)
- [Project Structure](#project-structure)
- [Contributing](#contributing)
- [License](#license)
- [Author](#author)

---

## Features
- **Strongly-typed error representation** using C# records and enums
- **Fluent ErrorBuilder** for easy and safe error construction
- **Integration with ASP.NET Core** via `ProblemDetails`
- **Support for error severity levels** (`Error`, `Warning`, `Info`)
- **Format string validation** with runtime safety and caller info in exceptions
- **Unit tested and .NET 8 ready**

## Installation

Add a reference to the `Lukdrasil.Error` project or package in your solution:
### If using as a project reference
```
<PackageReference Include="Lukdrasil.Error" Version="x.y.z" />
```
Or clone the repository and add the project to your solution.
## Usage

### Basic Error Creation
```csharp
using Lukdrasil.Error;

var error = new Error("ERR001", ErrorSeverity.Error, "Something went wrong");
var problemDetails = error.ToProblemDetails();
```
### Error with Formatting
```csharp
using Lukdrasil.Error;

var error = new Error(
    "ERR_FORMAT",
    ErrorSeverity.Warning,
    string.Format("Invalid value for field '{0}' at line '{1}'", "Name", 42)
);
```
### Using ErrorBuilder (Fluent API)
```csharp
using Lukdrasil.Error;

var error = new ErrorBuilder()
    .WithCode("ERR_USER")
    .WithSeverity(ErrorSeverity.Error)
    .WithDescription("User with id {0} not found", userId)
    .Build();
```
### Using ErrorBuilder with Direct Text
```csharp
using Lukdrasil.Error;

var error = new ErrorBuilder()
    .WithCode("ERR_CUSTOM")
    .WithDescriptionText("A custom error occurred.")
    .Build();
```
### Exception Safety

If you use a format string with placeholders but do not provide arguments, the builder will throw an `ArgumentException` with caller info:
```csharp
using Lukdrasil.Error;

var error = new ErrorBuilder()
    .WithDescription("Field {0} is required");

error.Build(); // Throws ArgumentException with caller info
```
## Project Structure
- `Error` - Main error record with code, severity, and description
- `ErrorBuilder` - Fluent builder for safe error creation
- `BaseError` - Abstract base for custom error types
- `ErrorSeverity` - Enum for error levels
- Unit tests in `Error.Tests`

## Contributing

Contributions are welcome! Please open issues or submit pull requests for any features, bugfixes, or improvements.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/YourFeature`)
3. Commit your changes (`git commit -am 'Add new feature'`)
4. Push to the branch (`git push origin feature/YourFeature`)
5. Open a pull request

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.