// <copyright file="CreateCommand.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using KDigitalCLI.Settings;
using Spectre.Console.Cli;

namespace KDigitalCLI.Commands;

public class CreateCommand : Command<CreateCommandSettings>
{
    private readonly IMyService myService;

    public CreateCommand(IMyService myService)
    {
        this.myService = myService;
    }

    public override int Execute(CommandContext context, CreateCommandSettings settings)
    {
        if (settings.Regular)
        {
            Console.WriteLine("Regular Feature");
            return 0;
        }

        if (settings.Preview)
        {
            Console.WriteLine("Preview Feature");
            return 0;
        }

        Console.WriteLine("Unknown!!!");
        return 1;
    }
}
