// <copyright file="ProfileAlreadyExistsException.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Exceptions;

/// <summary>
/// Occurs when a profile already exists.
/// </summary>
public class ProfileAlreadyExistsException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileAlreadyExistsException"/> class.
    /// </summary>
    public ProfileAlreadyExistsException()
        : base("The profile already exists.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileAlreadyExistsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public ProfileAlreadyExistsException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileAlreadyExistsException"/> class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">
    ///     The <see cref="Exception"/> instance that caused the current exception.
    /// </param>
    public ProfileAlreadyExistsException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
