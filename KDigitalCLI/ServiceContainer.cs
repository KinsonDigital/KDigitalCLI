// <copyright file="ServiceContainer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using SimpleInjector;
using Spectre.Console.Cli;

namespace KDigitalCLI;

using System.IO.Abstractions;
using Commands.Settings;
using Factories;
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
        SetupCommands();

        this.container.Register<IHttpClientFactory, HttpClientFactory>(Lifestyle.Singleton);
        this.container.Register<IJsonService, JsonService>(Lifestyle.Singleton);
        this.container.Register<IFindDirService, FindDirService>(Lifestyle.Singleton);
        this.container.Register<ISecretService, SecretService>(Lifestyle.Singleton);
        this.container.Register(() => FileSystem.File, Lifestyle.Singleton);
        this.container.Register(() => FileSystem.Directory, Lifestyle.Singleton);
        this.container.Register(() => FileSystem.Path, Lifestyle.Singleton);
        this.container.Register(
            () => new IssueService(this.container.GetInstance<IHttpClientFactory>().CreateIssueClient()),
            Lifestyle.Singleton);

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

    private void SetupCommands()
    {
        this.container.Register<CreateFeatureSettings>(Lifestyle.Singleton);
        this.container.Register<EmptyCommandSettings>(Lifestyle.Singleton);
        this.container.Register<CreateProfileSettings>(Lifestyle.Singleton);
    }
}
