// <copyright file="AttemptedToSetUnSettableRowException.cs" company="Joe Braught">
// Copyright (c) Joe Braught. All rights reserved.
// </copyright>

[Serializable]
internal class AttemptedToSetUnSettableRowException : Exception
{
    public AttemptedToSetUnSettableRowException()
    {
    }

    public AttemptedToSetUnSettableRowException(string? message)
        : base(message)
    {
    }

    public AttemptedToSetUnSettableRowException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}