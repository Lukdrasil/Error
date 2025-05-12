using ElvenScript.Error;

namespace Tests.Common.Error;
public class ErrorTests
{
    [Fact]
    public void ToProblemDetails_ShouldReturnCorrectProblemDetails_WithDefaultSeverity()
    {
        // Arrange  
        var error = new ElvenScript.Error.Error("ERR001", "Default error description");

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
        var error = new ElvenScript.Error.Error("WARN001", "Warning description", ErrorSeverity.Warning);

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
        var error = new ElvenScript.Error.Error("INFO001", "Info description", ErrorSeverity.Info);

        // Act
        var problemDetails = error.ToProblemDetails();

        // Assert
        Assert.NotNull(problemDetails);
        Assert.Equal("INFO001", problemDetails.Title);
        Assert.Equal("Info description", problemDetails.Detail);
        Assert.Equal((int)ErrorSeverity.Info, problemDetails.Status);
    }
}