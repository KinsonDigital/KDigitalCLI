// <copyright file="CreateCommand.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands;

using Spectre.Console.Cli;

public class CreateCommand : Command
{
    public override int Execute(CommandContext context)
    {
        // TODO: Check if there are any remaining args and if not, throw error.
        // Must have a 'feature' or 'profile' argument.
        // Use context.RemainingArguments

        return 0;
    }
}
