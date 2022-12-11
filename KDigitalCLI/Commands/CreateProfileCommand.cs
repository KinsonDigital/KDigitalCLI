// <copyright file="CreateProfileCommand.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands;

using Settings;
using Spectre.Console.Cli;

public class CreateProfileCommand : Command<CreateProfileSettings>
{

    public override int Execute(CommandContext context, CreateProfileSettings settings)
    {
        if (string.IsNullOrEmpty(settings.Name))
        {
            Console.WriteLine("You must provide a profile name.");
            return -1;
        }

        Console.WriteLine($"Profile '{settings.Name}' created.");

        return 0;
    }
}
