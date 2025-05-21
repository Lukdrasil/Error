using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ElvenScript.Error;

public partial class ErrorBuilder
{
    private enum DescriptionMode { None, Text, Format }
    private string _code = string.Empty;
    private ErrorSeverity _severity = ErrorSeverity.Error;
    private string _description = string.Empty;
    private string _descriptionFormat = string.Empty;
    private object[] _args = Array.Empty<object>();
    private DescriptionMode _descriptionMode = DescriptionMode.None;

    public ErrorBuilder WithCode(string code)
    {
        _code = code;
        return this;
    }

    public ErrorBuilder WithSeverity(ErrorSeverity severity)
    {
        _severity = severity;
        return this;
    }

    public ErrorBuilder WithDescription(string descriptionFormat, params object[] args)
    {
        _descriptionFormat = descriptionFormat;
        _args = args;
        _descriptionMode = DescriptionMode.Format;
        if (args != null && args.Length > 0 && ContainsPlaceholders(descriptionFormat))
        {
            _description = string.Format(descriptionFormat, args);
        }
        else
        {
            _description = descriptionFormat;
        }
        return this;
    }

    public ErrorBuilder WithDescriptionText(string text)
    {
        _description = text;
        _descriptionFormat = text;
        _args = Array.Empty<object>();
        _descriptionMode = DescriptionMode.Text;
        return this;
    }

    public Error Build(params object[] args)
    {
        return BuildInternal(args);
    }

    private Error BuildInternal(
        object[] args,
        [CallerMemberName] string caller = "",
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        if (_descriptionMode == DescriptionMode.Text)
        {
            return new Error(_code, _severity, _description);
        }

        var useArgs = (args != null && args.Length > 0)
            ? args
            : _args;

        if (_descriptionMode == DescriptionMode.Format && ContainsPlaceholders(_descriptionFormat) && (useArgs == null || useArgs.Length == 0))
        {
            throw new ArgumentException($"Format string contains placeholders, but no arguments were provided. Caller: {caller} at {file}:{line}");
        }

        if (useArgs != null && useArgs.Length > 0 && ContainsPlaceholders(_descriptionFormat))
        {
            var desc = string.Format(_descriptionFormat, useArgs);
            return new Error(_code, _severity, desc);
        }

        return new Error(_code, _severity, _descriptionFormat);
    }

    private static bool ContainsPlaceholders(string format)
    {
        return !string.IsNullOrEmpty(format) && PlaceholderRegex().IsMatch(format);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Source generator method")]
    [System.Text.RegularExpressions.GeneratedRegex("\\{\\d+\\}")]
    private static partial Regex PlaceholderRegex();
}
