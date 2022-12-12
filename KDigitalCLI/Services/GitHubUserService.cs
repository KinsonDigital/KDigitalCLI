// <copyright file="GitHubUserService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services;

using Guards;
using Interfaces;
using Octokit;

/// <inheritdoc/>
public class GitHubUserService : IGitHubUserService
{
    private readonly IUsersClient userClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="GitHubUserService"/> class.
    /// </summary>
    /// <param name="userClient">A client for GitHub's Users API.</param>
    public GitHubUserService(IUsersClient userClient)
    {
        EnsureThat.ParamIsNotNull(userClient);

        this.userClient = userClient;
    }

    /// <inheritdoc/>
    public async Task<bool> UserExists(string loginName)
    {
        if (string.IsNullOrEmpty(loginName))
        {
            return false;
        }

        try
        {
            _ = await this.userClient.Get(loginName);
        }
        catch (NotFoundException)
        {
            return false;
        }

        return true;
    }
}
