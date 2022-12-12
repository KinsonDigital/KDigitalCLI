// <copyright file="RepoService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services;

using Interfaces;
using Octokit;

/// <inheritdoc/>
public class RepoService : IRepoService
{
    private readonly IRepositoriesClient repoClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepoService"/> class.
    /// </summary>
    /// <param name="repoClient">A client for GitHub's Repositories API.</param>
    public RepoService(IRepositoriesClient repoClient) => this.repoClient = repoClient;

    /// <inheritdoc/>
    public async Task<bool> RepoExists(string repoOwner, string repoName)
    {
        if (string.IsNullOrEmpty(repoOwner) || string.IsNullOrEmpty(repoName))
        {
            return false;
        }

        try
        {
            _ = await this.repoClient.Get(repoOwner, repoName);
        }
        catch (NotFoundException)
        {
            return false;
        }

        return true;
    }
}
