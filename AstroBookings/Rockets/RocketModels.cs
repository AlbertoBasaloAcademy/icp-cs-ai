namespace AstroBookings.Rockets;

public enum RocketRange
{
    Suborbital,
    Orbital,
    Moon,
    Mars
}

public sealed record Rocket(int Id, string Name, RocketRange Range, int Capacity);

public sealed record RocketDraft(string Name, RocketRange Range, int Capacity);

public sealed record RocketRequest(string? Name, string? Range, int Capacity);

public sealed record RocketResponse(int Id, string Name, string Range, int Capacity);
