// <copyright file="HttpClientFactoryTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLITests.Factories;

using FluentAssertions;
using KDigitalCLI.Factories;
using KDigitalCLI.Services.Interfaces;
using NSubstitute;

/// <summary>
/// Tests the <see cref="HttpClientFactory"/> class.
/// </summary>
public class HttpClientFactoryTests
{
    private readonly ISecretService mockSecretService;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientFactoryTests"/> class.
    /// </summary>
    public HttpClientFactoryTests()
    {
        this.mockSecretService = Substitute.For<ISecretService>();
    }

    #region Constructor Tests
    [Fact]
    public void Ctor_WithNullSecretServiceParam_ThrowsException()
    {
        // Arrange & Act
        var act = () =>
        {
            _ = new HttpClientFactory(this.mockSecretService);
        };

        // Assert
        act.Should()
            .Throw<ArgumentNullException>()
            .WithMessage("The parameter must not be null. (Parameter 'secretService')");
    }
    #endregion

    #region Method Tests
    #endregion

    /// <summary>
    /// Creates a new instance of <see cref="HttpClientFactory"/> for the purpose of testing.
    /// </summary>
    /// <returns>The instance to test.</returns>
    private HttpClientFactory CreateFactory()
        => new (this.mockSecretService);
}
