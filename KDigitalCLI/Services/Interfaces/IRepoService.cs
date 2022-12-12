// <copyright file="IRepoService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services.Interfaces;

public interface IRepoService
{
    Task<bool> RepoExists(string repoOwner, string repoName);
}
