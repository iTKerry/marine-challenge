﻿namespace WebAPP.Common;

public class Envelope<T>
{
    public T Result { get; }
    public string? ErrorMessage { get; }
    public DateTime TimeGenerated { get; }

    public Envelope(T result, string? errorMessage)
    {
        Result = result;
        ErrorMessage = errorMessage;
        TimeGenerated = DateTime.UtcNow;
    }
}

public  class Envelope : Envelope<string?>
{
    public Envelope(string? errorMessage)
        : base(null, errorMessage)
    {
    }

    public static Envelope<T> Ok<T>(T result)
    {
        return new Envelope<T>(result, null);
    }

    public static Envelope Ok()
    {
        return new Envelope(null);
    }

    public static Envelope Error(string? errorMessage)
    {
        return new Envelope(errorMessage);
    }
}