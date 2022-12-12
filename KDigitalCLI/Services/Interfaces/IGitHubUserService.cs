// <copyright file="IGitHubUserService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services.Interfaces;

public interface IGitHubUserService
{
    Task<bool> UserExists(string loginName);
}
