using Lukdrasil.Error;

namespace Tests.Common.Error;
public class ErrorTests
{
    [Fact]
    public void ToProblemDetails_ShouldReturnCorrectProblemDetails_WithDefaultSeverity()
    {
        // Arrange
        var error = new Lukdrasil.Error.Error("ERR001", ErrorSeverity.Error, "Default error description");

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("ERR001", problemDetails.Title);
        Assert.Equal("Default error description", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Error, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_ShouldReturnCorrectProblemDetails_WithWarningSeverity()
    {
        // Arrange
        var error = new Lukdrasil.Error.Error("WARN001", ErrorSeverity.Warning, "Warning description");

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("WARN001", problemDetails.Title);
        Assert.Equal("Warning description", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Warning, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_ShouldReturnCorrectProblemDetails_WithInfoSeverity()
    {
        // Arrange
        var error = new Lukdrasil.Error.Error("INFO001", ErrorSeverity.Info, "Info description");

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("INFO001", problemDetails.Title);
        Assert.Equal("Info description", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Info, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithFormatArguments_ShouldFormatDescriptionCorrectly()
    {
        // Arrange
        var desc = string.Format("Error in field {0} at line {1}", "name", 42);
        var error = new Lukdrasil.Error.Error("ERR_FORMAT", ErrorSeverity.Error, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("ERR_FORMAT", problemDetails.Title);
        Assert.Equal("Error in field name at line 42", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Error, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithFormatArgumentsAndSeverity_ShouldFormatDescriptionAndSetSeverityCorrectly()
    {
        // Arrange
        var desc = string.Format("Error in field {0} at line {1}", "surname", 99);
        var error = new Lukdrasil.Error.Error("ERR_FORMAT2", ErrorSeverity.Warning, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("ERR_FORMAT2", problemDetails.Title);
        Assert.Equal("Error in field surname at line 99", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Warning, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithoutCode_ShouldSetCodeToEmptyString()
    {
        // Arrange
        var error = new Lukdrasil.Error.Error(string.Empty, ErrorSeverity.Error, "Description only error");

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal(string.Empty, problemDetails.Title);
        Assert.Equal("Description only error", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Error, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithoutCode_UsingFormat_ShouldSetCodeToEmptyStringAndFormatDescription()
    {
        // Arrange
        var desc = string.Format("Error in field {0}", "address");
        var error = new Lukdrasil.Error.Error(string.Empty, ErrorSeverity.Error, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal(string.Empty, problemDetails.Title);
        Assert.Equal("Error in field address", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Error, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithoutCode_UsingFormatAndSeverity_ShouldSetCodeToEmptyStringAndFormatDescriptionAndSeverity()
    {
        // Arrange
        var desc = string.Format("Error in field {0}", "phone");
        var error = new Lukdrasil.Error.Error(string.Empty, ErrorSeverity.Info, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal(string.Empty, problemDetails.Title);
        Assert.Equal("Error in field phone", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Info, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithCodeAndSeverity_ShouldSetCodeAndSeverityCorrectly()
    {
        // Arrange
        var desc = string.Format("Custom error at {0}", 123);
        var error = new Lukdrasil.Error.Error("ERR_CUSTOM", ErrorSeverity.Warning, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("ERR_CUSTOM", problemDetails.Title);
        Assert.Equal("Custom error at 123", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Warning, problemDetails.Status);
    }
}