// <copyright file="IssueServiceTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLITests.Services;

using FluentAssertions;
using KDigitalCLI.Services;
using NSubstitute;
using Octokit;

/// <summary>
/// Tests the <see cref="IssueService"/> class.
/// </summary>
public class IssueServiceTests
{
    private readonly IIssuesClient mockIssueClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="IssueServiceTests"/> class.
    /// </summary>
    public IssueServiceTests() => this.mockIssueClient = Substitute.For<IIssuesClient>();

    #region Method Tests
    [Fact]
    public async void IssueNumberValid_WithValidIssueNumber_ReturnsTrue()
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.IssueNumberValid(123);

        // Assert
        actual.Should().BeTrue();
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="IssueService"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private IssueService CreateSystemUnderTest()
        => new (this.mockIssueClient);
}
