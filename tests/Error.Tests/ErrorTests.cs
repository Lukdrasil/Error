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
        Assert.Equal(500, problemDetails.Status);
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
        Assert.Equal(400, problemDetails.Status);
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
        Assert.Equal(400, problemDetails.Status);
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
        Assert.Equal(500, problemDetails.Status);
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
        Assert.Equal(400, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithoutCode_ShouldUseReasonPhraseAsTitle()
    {
        // Arrange
        var error = new Lukdrasil.Error.Error(string.Empty, ErrorSeverity.Error, "Description only error");

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("Internal Server Error", problemDetails.Title);
        Assert.Equal("Description only error", problemDetails.Detail);
        Assert.Equal(500, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithoutCode_UsingFormat_ShouldUseReasonPhraseAsTitleAndFormatDescription()
    {
        // Arrange
        var desc = string.Format("Error in field {0}", "address");
        var error = new Lukdrasil.Error.Error(string.Empty, ErrorSeverity.Error, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("Internal Server Error", problemDetails.Title);
        Assert.Equal("Error in field address", problemDetails.Detail);
        Assert.Equal(500, problemDetails.Status);
    }

    [Fact]
    public void Constructor_WithoutCode_UsingFormatAndSeverity_ShouldUseReasonPhraseAsTitleAndFormatDescriptionAndSeverity()
    {
        // Arrange
        var desc = string.Format("Error in field {0}", "phone");
        var error = new Lukdrasil.Error.Error(string.Empty, ErrorSeverity.Info, desc);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("Bad Request", problemDetails.Title);
        Assert.Equal("Error in field phone", problemDetails.Detail);
        Assert.Equal(400, problemDetails.Status);
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
        Assert.Equal(400, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_ErrorSeverity_SetsStatus500()
    {
        var error = new Lukdrasil.Error.Error("E1", ErrorSeverity.Error, "d");

        var problemDetails = error.ToProblemDetails();

        Assert.Equal(500, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_WarningSeverity_SetsStatus400()
    {
        var error = new Lukdrasil.Error.Error("W1", ErrorSeverity.Warning, "d");

        var problemDetails = error.ToProblemDetails();

        Assert.Equal(400, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_InfoSeverity_SetsStatus400()
    {
        var error = new Lukdrasil.Error.Error("I1", ErrorSeverity.Info, "d");

        var problemDetails = error.ToProblemDetails();

        Assert.Equal(400, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_UndefinedSeverity_FallsBackToStatus500()
    {
        var error = new Lukdrasil.Error.Error("X1", (ErrorSeverity)99, "d");

        var problemDetails = error.ToProblemDetails();

        Assert.Equal(500, problemDetails.Status);
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_NeverReturnsZeroStatus()
    {
        foreach (var severity in Enum.GetValues<ErrorSeverity>())
        {
            var error = new Lukdrasil.Error.Error("S1", severity, "d");

            var problemDetails = error.ToProblemDetails();

            Assert.NotNull(problemDetails.Status);
            Assert.InRange(problemDetails.Status!.Value, 400, 599);
        }
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_EmptyCode_UsesReasonPhraseAsTitle()
    {
        var error = new Lukdrasil.Error.Error("", ErrorSeverity.Error, "d");

        var problemDetails = error.ToProblemDetails();

        Assert.Equal("Internal Server Error", problemDetails.Title);
    }

    [Fact]
    public void ToProblemDetails_Rfc9457_WhitespaceCode_UsesReasonPhraseAsTitle()
    {
        var error = new Lukdrasil.Error.Error("   ", ErrorSeverity.Warning, "d");

        var problemDetails = error.ToProblemDetails();

        Assert.Equal("Bad Request", problemDetails.Title);
    }
}