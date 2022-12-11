// See https://aka.ms/new-console-template for more information

using KDigitalCLI;
using KDigitalCLI.Commands;
using Octokit;
using Octokit.Internal;
using Spectre.Console.Cli;

var app = new CommandApp(new ServiceContainer());

app.Configure(config =>
{
    config.AddBranch("create", create =>
    {
        create.AddCommand<CreateProfileCommand>("profile")
            .WithDescription("Creates a new profile.")
            .WithExample(new[] { "profile", "-n=MyProfile" })
            .WithExample(new[] { "profile", "--name=MyProfile" });

        create.AddCommand<CreateFeatureCommand>("feature")
            .WithDescription("Creates a regular or preview feature.")
            .WithExample(new[] { "feature", "-n=John", "-t=regular" })
            .WithExample(new[] { "feature", "--name=John", "--type=preview" });
    });
});

var result = app.Run(args);

#if DEBUG
Console.ReadLine();
#endif

return result;
