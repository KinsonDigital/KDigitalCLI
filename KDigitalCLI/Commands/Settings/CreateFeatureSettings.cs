// <copyright file="CreateFeatureSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands.Settings;

using Spectre.Console.Cli;

public class CreateFeatureSettings : CreateSettings
{
    [CommandOption("-n|--name <NAME>")]
    public string Name { get; set; }

    [CommandOption("-t|--type <TYPE>")]
    public string Type { get; set; }
}
