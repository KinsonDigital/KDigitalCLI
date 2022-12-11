// <copyright file="ProfileAlreadyExistsExceptionTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLITests.Exceptions;

using FluentAssertions;
using KDigitalCLI.Exceptions;

/// <summary>
/// Tests the <see cref="ProfileAlreadyExistsException"/> class.
/// </summary>
public class ProfileAlreadyExistsExceptionTests
{
    #region Constructor Tests
    [Fact]
    public void Ctor_WithNoParam_CorrectlySetsExceptionMessage()
    {
        // Act
        var exception = new ProfileAlreadyExistsException();

        // Assert
        exception.Message.Should().Be("The profile already exists.");
    }

    [Fact]
    public void Ctor_WhenInvokedWithSingleMessageParam_CorrectlySetsMessage()
    {
        // Act
        var exception = new ProfileAlreadyExistsException("test-message");

        // Assert
        exception.Message.Should().Be("test-message");
    }

    [Fact]
    public void Ctor_WhenInvokedWithMessageAndInnerException_ThrowsException()
    {
        // Arrange
        var innerException = new Exception("inner-exception");

        // Act
        var deviceException = new ProfileAlreadyExistsException("test-exception", innerException);

        // Assert
        deviceException.InnerException.Message.Should().Be("inner-exception");
        deviceException.Message.Should().Be("test-exception");
    }
    #endregion
}
