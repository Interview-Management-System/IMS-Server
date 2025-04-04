﻿namespace InterviewManagementSystem.Domain.Shared.Exceptions;

public class DomainException(string message) : Exception(message)
{

    internal static void ThrowIfInvalidOperation(bool isInvalidCondition, string? errorMessage = null)
    {
        if (!isInvalidCondition)
            throw new InvalidOperationException(errorMessage ?? "Invalid operation");
    }

    internal static void ThrowIfInvalidArgument(bool isInvalidCondition, string? errorMessage = null)
    {
        if (isInvalidCondition)
            throw new ArgumentException(errorMessage ?? "Invalid argument");
    }
}
