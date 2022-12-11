// <copyright file="IIssueService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services.Interfaces;

public interface IIssueService
{
    Task<bool> IssueNumberValid(int issueNumber);
}
