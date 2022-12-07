// See https://aka.ms/new-console-template for more information

using KDigitalCLI;
using KDigitalCLI.Commands;
using Spectre.Console.Cli;

var app = new CommandApp(new ServiceContainer());
// app.SetDefaultCommand<EmptyCommand>();

app.Configure(config =>
{
    config.AddCommand<CreateCommand>("create")
        .WithDescription("Interactively creates a regular or preview feature.")
        .WithExample(new[] { "feature", "--regular" })
        .WithExample(new[] { "feature", "--preview" });
});

app.Run(args);

Console.ReadKey();
