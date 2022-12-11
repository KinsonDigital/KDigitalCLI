// <copyright file="IssueService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using KDigitalCLI.Services.Interfaces;

namespace KDigitalCLI.Services;

using Octokit;

public class IssueService : IIssueService
{
    private readonly IIssuesClient issueClient;

    public IssueService(IIssuesClient issueClient)
    {
        this.issueClient = issueClient;
    }

    public async Task<bool> IssueNumberValid(int issueNumber)
    {
        var issues = await this.issueClient.GetAllForRepository("", "");

        return issues.Any(i => i.Number == issueNumber);
    }
}
