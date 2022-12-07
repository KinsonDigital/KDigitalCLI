// <copyright file="MyService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDigitalCLI;

public class MyService : IMyService
{
    public int Add(int num1, int num2)
    {
        return num1 + num2;
    }
}
