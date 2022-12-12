// <copyright file="RepoServiceTests.cs" company="KinsonDigital">
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
/// Tests the <see cref="RepoService"/> class.
/// </summary>
public class RepoServiceTests
{
    private const string RepoOwner = "test-owner";
    private const string RepoName = "test-repo";
    private readonly IRepositoriesClient mockRepoClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepoServiceTests"/> class.
    /// </summary>
    public RepoServiceTests() => this.mockRepoClient = Substitute.For<IRepositoriesClient>();

    #region Constructor Tests
    [Fact]
    public void Ctor_WithNullRepoClientParam_ThrowsException()
    {
        // Arrange & Act
        var act = () =>
        {
            _ = new RepoService(null);
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'repoClient')");
    }
    #endregion

    #region Method Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void RepoExists_WithNullOrEmptyRepoOwnerParam_ReturnsFalse(string owner)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.RepoExists(owner, RepoName);

        // Assert
        actual.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async void RepoExists_WithNullOrEmptyRepoNameParam_ReturnsFalse(string name)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.RepoExists(RepoOwner, name);

        // Assert
        actual.Should().BeFalse();
    }

    [Fact]
    public async void RepoExists_WhenRepoExists_ReturnsTrue()
    {
        // Arrange
        this.mockRepoClient.Get(Arg.Any<string>(), Arg.Any<string>())
            .Returns(new Repository());
        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.RepoExists(RepoOwner, RepoName);

        // Assert
        actual.Should().BeTrue();
        await this.mockRepoClient.Received(1).Get(RepoOwner, RepoName);
    }

    [Fact]
    public async void RepoExists_WhenRepoDoesNotExist_ReturnsFalse()
    {
        // Arrange
        this.mockRepoClient.Get(Arg.Any<string>(), Arg.Any<string>())
            .ThrowsAsync(_ => throw new NotFoundException(Arg.Any<string>(), HttpStatusCode.NotFound));

        var sut = CreateSystemUnderTest();

        // Act
        var actual = await sut.RepoExists(RepoOwner, RepoName);

        // Assert
        actual.Should().BeFalse();
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="RepoService"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private RepoService CreateSystemUnderTest()
        => new (this.mockRepoClient);
}
