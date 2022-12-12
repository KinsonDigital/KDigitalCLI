// <copyright file="CreateProfileSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands.Settings;

using Spectre.Console.Cli;

public class CreateProfileSettings : CreateSettings
{
    [CommandOption("-n|--name <NAME>")]
    public string Name { get; set; }
}
