// <copyright file="HttpClientFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Factories;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Guards;
using Octokit;
using Octokit.Internal;
using Services.Interfaces;

/// <inheritdoc/>
[ExcludeFromCodeCoverage]
internal sealed class HttpClientFactory : IHttpClientFactory
{
    private const string ProductName = "KDCLI";
    private const string SecretName = "GitHubApiToken";
    private readonly IGitHubClient githubClient;
    private IIssuesClient? issueClient;
    private IUsersClient? userClient;
    private IRepositoriesClient? repoClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpClientFactory"/> class.
    /// </summary>
    /// <param name="secretService">Provides access to local secrets.</param>
    /// <exception cref="ArgumentNullException">
    ///     Occurs if any of the arguments are <c>null</c>.
    /// </exception>
    public HttpClientFactory(ISecretService secretService)
    {
        EnsureThat.ParamIsNotNull(secretService, nameof(secretService));

        // TODO: Throw exception if the token is null/empty
        var token = secretService.LoadSecret(SecretName);

        var productHeaderValue = new ProductHeaderValue(ProductName);
        var credStore = new InMemoryCredentialStore(new Credentials(token));

        this.githubClient = new GitHubClient(productHeaderValue, credStore);
    }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">
    /// Thrown for the following reasons:
    ///     <list type="bullet">
    ///         <item>The internal product name is null or empty.</item>
    ///         <item>The token is null or empty.</item>
    ///     </list>
    /// </exception>
    public IGitHubClient CreateGitHubClient() => this.githubClient;

    /// <inheritdoc/>
    public IIssuesClient CreateIssuesClient() => this.issueClient ??= this.githubClient.Issue;

    /// <inheritdoc/>
    public IUsersClient CreateUserClient() => this.userClient ??= this.githubClient.User;

    /// <inheritdoc/>
    public IRepositoriesClient CreateRepoClient() => this.repoClient ??= this.githubClient.Repository;
}
