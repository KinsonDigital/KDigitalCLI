// <copyright file="CreateCommandSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System.ComponentModel;
using Spectre.Console.Cli;

namespace KDigitalCLI.Settings;

public class CreateCommandSettings : CommandSettings
{
    [CommandArgument(0, "<feature>")]
    public string CreateFeature { get; set; }

    [CommandOption("--regular")]
    public bool Regular { get; set; }

    [CommandOption("--preview")]
    public bool Preview { get; set; }
}
