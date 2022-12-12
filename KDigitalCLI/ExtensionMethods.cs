using System.Text.RegularExpressions;

namespace KDigitalCLI;

/// <summary>
/// Provides helper methods throughout the project.
/// </summary>
public static class ExtensionMethods
{
    private const char WinDirSeparatorChar = '\\';
    private const char CrossPlatDirSeparatorChar = '/';
    private const char MatchNumbers = '#';
    private const char MatchAnything = '*';


    /// <summary>
    /// Returns a value indicating whether or not the current <c>string</c> matches the given <paramref name="pattern"/>.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <param name="pattern">The pattern to check against the <c>string</c> <paramref name="value"/>.</param>
    /// <returns><c>true</c> if the <paramref name="pattern"/> is equal to the branch name.</returns>
    /// <remarks>
    ///     The comparison is case sensitive.
    /// </remarks>
    public static bool EqualTo(this string value, string pattern)
    {
        pattern = string.IsNullOrEmpty(pattern) ? string.Empty : pattern;

        var hasGlobbingSyntax = pattern.Contains(MatchNumbers) || pattern.Contains(MatchAnything);
        var isEqual = hasGlobbingSyntax
            ? Match(value, pattern)
            : (string.IsNullOrEmpty(pattern) && string.IsNullOrEmpty(value)) || pattern == value;

        return isEqual;
    }

    /// <summary>
    /// Converts the given <paramref name="path"/> to a cross platform path.
    /// </summary>
    /// <param name="path">The file or directory path.</param>
    /// <returns>The cross platform version of the <paramref name="path"/>.</returns>
    /// <returns>
    ///     This changes all '\' characters to '/' characters.
    ///     The '/' directory separator is valid on Windows and Linux systems.
    /// </returns>
    public static string ToCrossPlatPath(this string path) => path.Replace(WinDirSeparatorChar, CrossPlatDirSeparatorChar);

    /// <summary>
    /// Returns a value indicating whether or not the given <paramref name="globbingPattern"/> contains a match
    /// to the given <c>string</c> <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The <c>string</c> to match.</param>
    /// <param name="globbingPattern">The globbing pattern and text to search.</param>
    /// <returns>
    ///     <c>true</c> if the globbing pattern finds a match in the given <c>string</c> <paramref name="value"/>.
    /// </returns>
    private static bool Match(string value, string globbingPattern)
    {
        // NOTE: Refer to this website for more regex information -> https://regex101.com/
        const char regexMatchStart = '^';
        const char regexMatchEnd = '$';
        const string regexMatchNumbers = @"\d+";
        const string regexMatchAnything = ".+";

        // Remove any consecutive '#' and '*' symbols until no more consecutive symbols exist
        globbingPattern = RemoveConsecutiveCharacters(new[] { MatchNumbers, MatchAnything }, globbingPattern);

        // Replace the '#' symbol with
        globbingPattern = globbingPattern.Replace(MatchNumbers.ToString(), regexMatchNumbers);

        // Prefix all '.' symbols with '\' to match the '.' literally in regex
        globbingPattern = globbingPattern.Replace(".", @"\.");

        // Replace all '*' character with '.+'
        globbingPattern = globbingPattern.Replace(MatchAnything.ToString(), regexMatchAnything);

        globbingPattern = $"{regexMatchStart}{globbingPattern}{regexMatchEnd}";

        return Regex.Matches(value, globbingPattern).Count > 0;
    }

    /// <summary>
    /// Removes any consecutive occurrences of the given <paramref name="characters"/> from the given <c>string</c> <paramref name="value"/>.
    /// </summary>
    /// <param name="characters">The <c>char</c> to check.</param>
    /// <param name="value">The value that contains the consecutive characters to remove.</param>
    /// <returns>The original <c>string</c> value with the consecutive characters removed.</returns>
    private static string RemoveConsecutiveCharacters(IEnumerable<char> characters, string value)
    {
        foreach (var c in characters)
        {
            while (value.Contains($"{c}{c}"))
            {
                value = value.Replace($"{c}{c}", c.ToString());
            }
        }

        return value;
    }
}
