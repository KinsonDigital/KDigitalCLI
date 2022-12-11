// <copyright file="CreateSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands.Settings;

using Spectre.Console.Cli;

public class CreateSettings : CommandSettings
{
    [CommandArgument(0, "[create]")]
    public string Create { get; set; }
}
