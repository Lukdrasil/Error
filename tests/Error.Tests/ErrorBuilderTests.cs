using ElvenScript.Error;

namespace Tests.Common.Error;

public class ErrorBuilderTests
{
    [Fact]
    public void Build_ShouldCreateError_WithAllPropertiesSet()
    {
        var error = new ErrorBuilder()
            .WithCode("ERR_BUILDER")
            .WithSeverity(ErrorSeverity.Warning)
            .WithDescription("Builder error at {0}", 123)
            .Build();

        Assert.Equal("ERR_BUILDER", error.Code);
        Assert.Equal(ErrorSeverity.Warning, error.Severity);
        Assert.Equal("Builder error at 123", error.Description);
    }

    [Fact]
    public void Build_ShouldCreateError_WithDefaultValues()
    {
        var error = new ErrorBuilder().Build();
        Assert.Equal(string.Empty, error.Code);
        Assert.Equal(ErrorSeverity.Error, error.Severity);
        Assert.Equal(string.Empty, error.Description);
    }

    [Fact]
    public void FluentSyntax_ShouldAllowChaining()
    {
        var builder = new ErrorBuilder();
        builder.WithCode("CHAIN").WithSeverity(ErrorSeverity.Info).WithDescription("Chained {0}", "desc");
        var error = builder.Build();
        Assert.Equal("CHAIN", error.Code);
        Assert.Equal(ErrorSeverity.Info, error.Severity);
        Assert.Equal("Chained desc", error.Description);
    }

    [Fact]
    public void Build_WithArgsAtBuildTime_ShouldFormatDescriptionCorrectly()
    {
        var builder = new ErrorBuilder()
            .WithCode("ERR_LATE")
            .WithSeverity(ErrorSeverity.Error)
            .WithDescription("Late arg {0} and {1}");
        var error = builder.Build("foo", 42);
        Assert.Equal("ERR_LATE", error.Code);
        Assert.Equal(ErrorSeverity.Error, error.Severity);
        Assert.Equal("Late arg foo and 42", error.Description);
    }

    [Fact]
    public void WithDescription_WithoutArgs_ThenBuildWithoutArgs_ShouldKeepFormatString()
    {
        var builder = new ErrorBuilder()
            .WithDescription("No args here");
        var error = builder.Build();
        Assert.Equal("No args here", error.Description);
    }

    [Fact]
    public void WithDescription_WithArgsAndNoPlaceholders_ShouldIgnoreArgsAndUseFormat()
    {
        var builder = new ErrorBuilder()
            .WithDescription("No placeholders", 1, 2);
        var error = builder.Build();
        Assert.Equal("No placeholders", error.Description);
    }

    [Fact]
    public void WithDescriptionText_ShouldSetDescriptionDirectly()
    {
        var builder = new ErrorBuilder()
            .WithCode("ERR_TEXT")
            .WithDescriptionText("This is direct text");
        var error = builder.Build();
        Assert.Equal("ERR_TEXT", error.Code);
        Assert.Equal("This is direct text", error.Description);
    }

    [Fact]
    public void WithDescriptionText_ShouldIgnoreArgsAndFormat()
    {
        var builder = new ErrorBuilder()
            .WithDescriptionText("Direct text");
        var error = builder.Build("should", "be", "ignored");
        Assert.Equal("Direct text", error.Description);
    }

    [Fact]
    public void Build_ShouldThrowException_WhenFormatModeAndNoArgsProvided()
    {
        var builder = new ErrorBuilder()
            .WithCode("ERR_FORMAT")
            .WithSeverity(ErrorSeverity.Error)
            .WithDescription("Test {0} placeholder");
        var ex = Assert.Throws<ArgumentException>(() => builder.Build());
        Assert.Contains("placeholders", ex.Message);
    }

    [Fact]
    public void WithDescription_WithPlaceholdersButNoArgs_ShouldThrowException()
    {
        var builder = new ErrorBuilder()
            .WithDescription("Test {0} placeholder");
        var ex = Assert.Throws<ArgumentException>(() => builder.Build());
        Assert.Contains("placeholders", ex.Message);
    }
}
