// <copyright file="ExtensionMethodsTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using FluentAssertions;
using KDigitalCLI;

namespace KDigitalCLITests;

/// <summary>
/// Tests the the <see cref="ExtensionMethods"/> class.
/// </summary>
public class ExtensionMethodsTests
{
    #region Method Tests
    [Theory]
    [InlineData("preview/v1.2.3-preview.4", "preview/v#.#.#-preview.#", true)]
    [InlineData("feature/123-my-branch", "feature/123-my-branch", true)]
    [InlineData("", "", true)]
    [InlineData("", null, true)]
    [InlineData(null, "", true)]
    [InlineData(null, null, true)]
    public void EqualTo_WithCorrectPatternMatch_ReturnsTrue(
        string value,
        string pattern,
        bool expected)
    {
        // Act
        var act = value.EqualTo(pattern);

        // Assert
        act.Should().Be(expected);
    }

    [Theory]
    [InlineData(@"C:\dir-1\dir-2", "C:/dir-1/dir-2")]
    [InlineData(@"C:\dir-1\dir-2\", "C:/dir-1/dir-2/")]
    public void ToCrossPlatPath_WhenInvoked_ReturnsCorrectResult(string path, string expected)
    {
        // Act
        var actual = path.ToCrossPlatPath();

        // Assert
        actual.Should().Be(expected);
    }
    #endregion
}
