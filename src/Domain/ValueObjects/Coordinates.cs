namespace Domain.ValueObjects;

public sealed record Coordinates(Latitude Latitude, Longitude Longitude);

public sealed record Latitude
{
    public double Value { get; }
    
    public Latitude(double value)
    {
        if (Math.Abs(value) > 90)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }
}

public sealed record Longitude
{
    public double Value { get; }
    
    public Longitude(double value)
    {
        if (Math.Abs(value) > 180)
            throw new ArgumentOutOfRangeException(nameof(value));

        Value = value;
    }
}