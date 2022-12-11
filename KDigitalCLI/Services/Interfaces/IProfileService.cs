// <copyright file="IProfileService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services.Interfaces;

/// <summary>
/// Provides the management of application profiles.
/// </summary>
public interface IProfileService
{
    /// <summary>
    /// Creates a new profile with the given profile <paramref name="name"/> that contains the
    /// profile settings <paramref name="ownerName"/> and <paramref name="repoName"/>.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <param name="ownerName">The owner of a repository.</param>
    /// <param name="repoName">The name of the repository.</param>
    /// <remarks>
    ///     The <paramref name="ownerName"/> can also be the name of an GitHub organization.
    /// </remarks>
    void CreateProfile(string name, string ownerName, string repoName);

    /// <summary>
    /// Returns a value indicating if a profile that matches the given profile <paramref name="name"/> exists.
    /// </summary>
    /// <param name="name">The name of the profile.</param>
    /// <returns><c>true</c> if the profile exists.</returns>
    bool ProfileExists(string name);
}
