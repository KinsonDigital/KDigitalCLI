// <copyright file="ServiceContainer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using SimpleInjector;
using Spectre.Console.Cli;

namespace KDigitalCLI;

using System.IO.Abstractions;
using Commands.Settings;
using Factories;
using Octokit;
using Services;
using Services.Interfaces;

internal sealed class ServiceContainer : ITypeRegistrar
{
    private readonly Container container;
    private static readonly FileSystem FileSystem = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="ServiceContainer"/> class.
    /// </summary>
    public ServiceContainer() => this.container = new Container();

    public ITypeResolver Build()
    {
        SetupCommandSettings();
        SetupServices();

        this.container.Register<IHttpClientFactory, HttpClientFactory>(Lifestyle.Singleton);
        this.container.Register(() => FileSystem.File, Lifestyle.Singleton);
        this.container.Register(() => FileSystem.Directory, Lifestyle.Singleton);
        this.container.Register(() => FileSystem.Path, Lifestyle.Singleton);

        this.container.Verify();

        return new TypeResolver(this.container);
    }

    public void Register(Type service, Type implementation)
    {
        this.container.Register(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        this.container.RegisterInstance(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        this.container.Register(service, factory);
    }

    private void SetupCommandSettings()
    {
        this.container.Register<CreateFeatureSettings>(Lifestyle.Singleton);
        this.container.Register<EmptyCommandSettings>(Lifestyle.Singleton);
        this.container.Register<CreateProfileSettings>(Lifestyle.Singleton);
    }

    private void SetupServices()
    {
        this.container.Register<IJsonService, JsonService>(Lifestyle.Singleton);
        this.container.Register<IFindDirService, FindDirService>(Lifestyle.Singleton);
        this.container.Register<IProfileService, ProfileService>();
        this.container.Register<ISecretService, SecretService>(Lifestyle.Singleton);
        this.container.Register<IRepoService>(
            () => new RepoService(this.container.GetInstance<IHttpClientFactory>().CreateRepoClient()));
        this.container.Register(
            () => new IssueService(this.container.GetInstance<IHttpClientFactory>().CreateIssuesClient()),
            Lifestyle.Singleton);
        this.container.Register<IGitHubUserService>(
            () => new GitHubUserService(this.container.GetInstance<IHttpClientFactory>().CreateUserClient()),
            Lifestyle.Singleton);
    }
}
