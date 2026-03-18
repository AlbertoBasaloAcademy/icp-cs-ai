namespace AstroBookings.Rockets;

public static class RocketMapping
{
    public static RocketResponse ToResponse(this Rocket rocket) =>
        new(rocket.Id, rocket.Name, rocket.Range.ToApiValue(), rocket.Capacity);

    public static string ToApiValue(this RocketRange range) => range switch
    {
        RocketRange.Suborbital => "suborbital",
        RocketRange.Orbital => "orbital",
        RocketRange.Moon => "moon",
        RocketRange.Mars => "mars",
        _ => throw new ArgumentOutOfRangeException(nameof(range), range, null)
    };

    public static bool TryParseRange(string value, out RocketRange range)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            range = default;
            return false;
        }

        switch (value.Trim().ToLowerInvariant())
        {
            case "suborbital":
                range = RocketRange.Suborbital;
                return true;
            case "orbital":
                range = RocketRange.Orbital;
                return true;
            case "moon":
                range = RocketRange.Moon;
                return true;
            case "mars":
                range = RocketRange.Mars;
                return true;
            default:
                range = default;
                return false;
        }
    }
}
