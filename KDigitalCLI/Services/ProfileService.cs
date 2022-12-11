// <copyright file="ProfileService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI.Services;

using System.IO.Abstractions;
using Exceptions;
using Guards;
using Interfaces;

/// <inheritdoc/>
internal sealed class ProfileService : IProfileService
{
    private const string ProfileDirName = "Profiles";
    private const string FileExtension = ".json";
    private readonly string fullProfilesDirPath;
    private readonly IJsonService jsonService;
    private readonly IDirectory directory;
    private readonly IFile file;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProfileService"/> class.
    /// </summary>
    /// <param name="jsonService">Provides JSON services.</param>
    /// <param name="directory">Manages directories.</param>
    /// <param name="file">Manages files.</param>
    public ProfileService(
        IJsonService jsonService,
        IDirectory directory,
        IFile file)
    {
        EnsureThat.ParamIsNotNull(jsonService);
        EnsureThat.ParamIsNotNull(directory);
        EnsureThat.ParamIsNotNull(file);

        this.jsonService = jsonService;
        this.directory = directory;
        this.file = file;

        var appDirPath = this.directory.GetCurrentDirectory().ToCrossPlatPath();
        this.fullProfilesDirPath = $"{appDirPath}/{ProfileDirName}";
    }

    /// <inheritdoc/>
    /// <exception cref="ProfileAlreadyExistsException">
    ///     Thrown if a profile with the given <paramref name="name"/> already exists.
    /// </exception>
    public void CreateProfile(string name, string ownerName, string repoName)
    {
        EnsureThat.StringParamIsNotNullOrEmpty(name);
        EnsureThat.StringParamIsNotNullOrEmpty(ownerName);
        EnsureThat.StringParamIsNotNullOrEmpty(repoName);

        if (this.directory.Exists(this.fullProfilesDirPath) is false)
        {
            this.directory.CreateDirectory(this.fullProfilesDirPath);
        }

        var fileName = $"{name}{FileExtension}";
        var filePath = $"{this.fullProfilesDirPath}/{fileName}";

        if (this.file.Exists(filePath) is false)
        {
            throw new ProfileAlreadyExistsException($"The profile '{name}' already exists.");
        }

        var newProfile = new Profile(name, ownerName, repoName);

        var jsonData = this.jsonService.Serialize(newProfile);

        this.file.WriteAllText(filePath, jsonData);
    }

    /// <inheritdoc/>
    public bool ProfileExists(string name)
    {
        EnsureThat.StringParamIsNotNullOrEmpty(name);

        var fileName = $"{name}{FileExtension}";
        var filePath = $"{this.fullProfilesDirPath}/{fileName}";

        return this.file.Exists(filePath);
    }
}
