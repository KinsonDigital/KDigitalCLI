// <copyright file="TypeResolver.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using SimpleInjector;
using Spectre.Console.Cli;

namespace KDigitalCLI;

public class TypeResolver : ITypeResolver
{
    private Container container;

    public TypeResolver(Container container)
    {
        this.container = container ?? throw new ArgumentNullException();
    }

    public object? Resolve(Type? type)
    {
        return this.container.GetInstance(type);
    }
}
