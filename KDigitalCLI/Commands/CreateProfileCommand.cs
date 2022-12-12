// <copyright file="CreateProfileCommand.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Commands;

using Factories;
using Octokit;
using Services.Interfaces;
using Settings;
using Spectre.Console;
using Spectre.Console.Cli;

internal sealed class CreateProfileCommand : Command<CreateProfileSettings>
{
    private const int errorCodeCategory = -100;
    private readonly IAnsiConsole console;
    private readonly IGitHubUserService userService;
    private readonly IRepoService repoService;
    private readonly IProfileService profileService;
    private readonly TextPrompt<string> repoOwnerPrompt;
    private readonly TextPrompt<string> repoNamePrompt;
    private string repoOwner = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProfileCommand"/> class.
    /// </summary>
    /// <param name="console">Represents a console.</param>
    public CreateProfileCommand(
        IAnsiConsole console,
        IGitHubUserService userService,
        IRepoService repoService,
        IProfileService profileService)
    {
        // TODO: Null checks and tests
        this.console = console;
        this.userService = userService;
        this.repoService = repoService;
        this.profileService = profileService;

        this.repoOwnerPrompt = new TextPrompt<string>("[springgreen3]What is the owner of the repository?[/]")
            .PromptStyle("silver");
        this.repoOwnerPrompt.Validator = ValidateRepoOwner;

        this.repoNamePrompt = new TextPrompt<string>("[springgreen3]What is the name of the repository?[/]")
            .PromptStyle("silver");
        this.repoNamePrompt.Validator = ValidateRepoName;

    }

    // TODO: code docs
    public override int Execute(CommandContext context, CreateProfileSettings settings)
    {
        if (string.IsNullOrEmpty(settings.Name))
        {
            this.console.WriteLine("You must provide a profile name.");
            return errorCodeCategory - 1;
        }

        if (this.profileService.ProfileExists(settings.Name))
        {
            this.console.Markup($"The profile '{settings.Name}' already exists. Please use another name.");
            return errorCodeCategory - 2;
        }

        this.repoOwner = this.console.Prompt(this.repoOwnerPrompt);

        var repoName = this.console.Prompt(this.repoNamePrompt);

        this.profileService.CreateProfile(settings.Name, this.repoOwner, repoName);

        return 0;
    }

    // TODO: code docs
    private ValidationResult ValidateRepoOwner(string repoOwner)
    {
        var userExists = this.userService.UserExists(repoOwner).Result;

        if (userExists)
        {
            return ValidationResult.Success();
        }

        return ValidationResult.Error($"The repository owner '{repoOwner}' does not exist.");
    }

    // TODO: code docs
    private ValidationResult ValidateRepoName(string repoName)
    {
        var repoExists = this.repoService.RepoExists(this.repoOwner, repoName).Result;

        if (repoExists)
        {
            return ValidationResult.Success();
        }

        return ValidationResult
            .Error($"The repository name '{repoName}' does not exist for the repository owner '{this.repoOwner}'.");
    }
}
