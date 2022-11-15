// See https://aka.ms/new-console-template for more information


using KDigitalCLI.Commands;
using Spectre.Cli;

var app = new CommandApp();

app.Configure(config =>
{
    config.AddCommand<StartFeatureCommand>("start-feature")
        .WithDescription("Interactively creates a regular or preview feature.")
        .WithExample(new[] { "kd", "start-feature" });
});
