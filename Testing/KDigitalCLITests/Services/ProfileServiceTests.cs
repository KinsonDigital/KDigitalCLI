// <copyright file="ProfileServiceTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLITests.Services;

using System.IO.Abstractions;
using FluentAssertions;
using KDigitalCLI;
using KDigitalCLI.Exceptions;
using KDigitalCLI.Services;
using KDigitalCLI.Services.Interfaces;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

/// <summary>
/// Tests the <see cref="ProfileService"/> class.
/// </summary>
public class ProfileServiceTests
{
    private const string ProfileDirName = "Profiles";
    private const string ProfileFileName = "test-profile.json";
    private const string AppDirPath = "C:/app-dir";
    private const string FullProfileDirPath = $"{AppDirPath}/{ProfileDirName}";
    private const string FullProfileFilePath = $"{FullProfileDirPath}/{ProfileFileName}";
    private const string TestProfile = "test-profile";
    private const string TestOwner = "test-owner";
    private const string TestRepo = "test-repo";
    private readonly IJsonService mockJsonService;
    private readonly IDirectory mockDirectory;
    private readonly IFile mockFile;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileServiceTests"/> class.
    /// </summary>
    public ProfileServiceTests()
    {
        this.mockJsonService = Substitute.For<IJsonService>();

        this.mockDirectory = Substitute.For<IDirectory>();
        this.mockDirectory.Exists(Arg.Any<string>()).Returns(true);
        this.mockDirectory.GetCurrentDirectory().Returns(AppDirPath);

        this.mockFile = Substitute.For<IFile>();
        this.mockFile.Exists(Arg.Any<string>()).Returns(true);
    }

    #region Constructor Tests

    [Fact]
    public void Ctor_WithNullJsonServiceParam_ThrowsException()
    {
        // Arrange & Act
        var act = () =>
        {
            _ = new ProfileService(
                null,
                this.mockDirectory,
                this.mockFile);
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'jsonService')");
    }

    [Fact]
    public void Ctor_WithNullDirectoryParam_ThrowsException()
    {
        // Arrange & Act
        var act = () =>
        {
            _ = new ProfileService(
                this.mockJsonService,
                null,
                this.mockFile);
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'directory')");
    }

    [Fact]
    public void Ctor_WithNullFileParam_ThrowsException()
    {
        // Arrange & Act
        var act = () =>
        {
            _ = new ProfileService(
                this.mockJsonService,
                this.mockDirectory,
                null);
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'file')");
    }
    #endregion

    #region Method Tests
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreateProfile_WithNullOrEmptyNameParam_ThrowsException(string profileName)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var act = () => sut.CreateProfile(profileName, null, null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("The string parameter must not be null or empty. (Parameter 'name')");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreateProfile_WithNullOrEmptyOwnerNameParam_ThrowsException(string ownerName)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var act = () => sut.CreateProfile(TestProfile, ownerName, null);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("The string parameter must not be null or empty. (Parameter 'ownerName')");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void CreateProfile_WithNullOrEmptyRepoNameParam_ThrowsException(string repoName)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var act = () => sut.CreateProfile(TestProfile, TestOwner, repoName);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("The string parameter must not be null or empty. (Parameter 'repoName')");
    }

    [Fact]
    public void CreateProfile_WhenProfileDirectoryDoesNotExit_CreateDirectory()
    {
        // Arrange
        this.mockDirectory.Exists(Arg.Any<string>()).Returns(false);

        var sut = CreateSystemUnderTest();

        // Act
        sut.CreateProfile(TestProfile, TestOwner, TestRepo);

        // Assert
        this.mockDirectory.Received(1).Exists(FullProfileDirPath);
        this.mockDirectory.Received(1).CreateDirectory(FullProfileDirPath);
    }

    [Fact]
    public void CreateProfile_WhenProfileWithTheSameNameAlreadyExists_ThrowException()
    {
        // Arrange
        this.mockFile.Exists(Arg.Any<string>()).Returns(false);

        var sut = CreateSystemUnderTest();

        // Act
        var act = () => sut.CreateProfile(TestProfile, TestOwner, TestRepo);

        // Assert
        act.Should().Throw<ProfileAlreadyExistsException>()
            .WithMessage($"The profile '{TestProfile}' already exists.");
    }

    [Fact]
    public void CreateProfile_WhenProfileDirPathExistsAndTheProfileFileDoesNotExist_CreatesProfile()
    {
        // Arrange
        var expectedProfile = new Profile(TestProfile, TestOwner, TestRepo);
        const string expectedJsonData = $$"""
            {
                "ProfileName": {{TestProfile}},
                "RepoOwner": {{TestOwner}},
                "RepoName": {{TestRepo}},
            }
            """;
        this.mockJsonService.Serialize(Arg.Any<Profile>()).Returns(expectedJsonData);
        var sut = CreateSystemUnderTest();

        // Act
        sut.CreateProfile(TestProfile, TestOwner, TestRepo);

        // Assert
        this.mockFile.Received(1).Exists(FullProfileFilePath);
        this.mockJsonService.Received(1).Serialize(expectedProfile);
        this.mockFile.Received(1).WriteAllText(FullProfileFilePath, expectedJsonData);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ProfileExists_WithNullOrEmptyName_ThrowsException(string profileName)
    {
        // Arrange
        var sut = CreateSystemUnderTest();

        // Act
        var act = () => sut.ProfileExists(profileName);

        // Assert
        act.Should().Throw<ArgumentNullException>()
            .WithMessage("The string parameter must not be null or empty. (Parameter 'name')");
    }

    [Theory]
    [InlineData(true, true)]
    [InlineData(false, false)]
    public void ProfileExists_WhenInvoked_ReturnsCorrectResult(bool exists, bool expected)
    {
        // Arrange
        this.mockFile.Exists(Arg.Any<string>()).Returns(exists);

        var sut = CreateSystemUnderTest();

        // Act
        var actual = sut.ProfileExists(TestProfile);

        // Assert
        actual.Should().Be(expected);
        this.mockFile.Received(1).Exists(FullProfileFilePath);
    }
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="ProfileService"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private ProfileService CreateSystemUnderTest()
        => new (this.mockJsonService, this.mockDirectory, this.mockFile);
}
