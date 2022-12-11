// <copyright file="CreateFeatureCommand.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands;

using Spectre.Console.Cli;
using Settings;

public class CreateFeatureCommand : Command<CreateFeatureSettings>
{
    public override int Execute(CommandContext context, CreateFeatureSettings settings)
    {
        if (string.IsNullOrEmpty(settings.Name))
        {
            Console.WriteLine("You must provide a feature name.");

            return -1;
        }

        if (string.IsNullOrEmpty(settings.Type))
        {
            Console.WriteLine("You must provide the type of feature to create with a value of 'regular' or 'preview'.");
            Console.WriteLine("Example: create feature --name=john --type=regular");
            return -2;
        }

        settings.Type = settings.Type.ToLower();

        if (settings.Type != "regular" && settings.Type != "preview")
        {
            Console.WriteLine("The type of feature must be 'regular' or 'preview'.");
            return -3;
        }

        var type = settings.Type == "regular"
            ? "Regular"
            : "Preview";

        if (settings.Type == "regular")
        {

            Console.WriteLine($"{type} feature '{settings.Name}' created.");
        }
        else if (settings.Type == "preview")
        {
            Console.WriteLine($"{type} feature '{settings.Name}' created.");
        }

        return 0;
    }
}
