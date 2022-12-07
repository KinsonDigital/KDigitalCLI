// <copyright file="ServiceContainer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using SimpleInjector;
using Spectre.Console.Cli;

namespace KDigitalCLI;

using Settings;

public class ServiceContainer : ITypeRegistrar
{
    private readonly Container container;

    public ServiceContainer()
    {
        this.container = new Container();
    }

    public ITypeResolver Build()
    {
        this.container.Register<IMyService, MyService>();
        this.container.Register<CreateCommandSettings>(Lifestyle.Singleton);
        this.container.Register<EmptyCommandSettings>(Lifestyle.Singleton);

        return new TypeResolver(container);
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
}
