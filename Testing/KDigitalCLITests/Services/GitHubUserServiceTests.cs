// <copyright file="GitHubUserServiceTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLITests.Services;

using System.Net;
using FluentAssertions;
using KDigitalCLI.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Octokit;

/// <summary>
/// Tests the <see cref="GitHubUserService"/> class.
/// </summary>
public class GitHubUserServiceTests
{
    private readonly IUsersClient userClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubUserServiceTests"/> class.
    /// </summary>
    public GitHubUserServiceTests()
    {
        this.userClient = Substitute.For<IUsersClient>();
    }

    #region Constructor Tests
    [Fact]
    public void Ctor_WithNullUserClientParam_ThrowsException()
    {
        // Arrange & Act
        var act = () =>
        {
            _ = new GitHubUserService(null);
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'userClient')");
    }
    #endregion

    #region Method Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void UserExists_WithNullOrEmptyLoginParam_ReturnsFalse(string login)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.UserExists(login);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public async void UserExists_WhenUserExists_ReturnsTrue()
    {
        // Arrange
        this.userClient.Get(Arg.Any<string>()).Returns(new User());

        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.UserExists("test-user");

        // Assert
        actual.Should().BeTrue();
        await this.userClient.Received(1).Get("test-user");
    }

    [Fact]
    public async void UserExists_WhenUserDoesNotExist_ReturnsFalse()
    {
        // Arrange
        this.userClient.Get(Arg.Any<string>())
            .ThrowsAsync(_ => throw new NotFoundException(Arg.Any<string>(), HttpStatusCode.NotFound));

        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.UserExists("non-existent-user");

        // Assert
        actual.Should().BeFalse();
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="GitHubUserService"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private GitHubUserService CreateSystemUnderTest()
        => new (this.userClient);
}
