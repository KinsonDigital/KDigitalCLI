// <copyright file="EmptyCommand.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands;

using Settings;
using Spectre.Console;
using Spectre.Console.Cli;

public class EmptyCommand : Command

{
    public override int Execute(CommandContext context)
    {
        var infoLines = new[]
        {
            "[green]Usage:[/] kdcli [[options]]",
            string.Empty,
            "[green]Options[/]",
            "\t",
            "\t",
        };

        foreach (var info in infoLines)
        {
            AnsiConsole.MarkupLine(info);
        }
        return 0;
    }
}
