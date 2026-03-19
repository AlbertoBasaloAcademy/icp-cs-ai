namespace AstroBookings.Rockets;

public enum AlcanceCohete
{
    Suborbital,
    Orbital,
    Luna,
    Marte
}

public sealed record Cohete(int Id, string Nombre, AlcanceCohete Alcance, int Capacidad);

public sealed record BorradorCohete(string Nombre, AlcanceCohete Alcance, int Capacidad);

public sealed record SolicitudCohete(string? Nombre, string? Alcance, int Capacidad);

public sealed record RespuestaCohete(int Id, string Nombre, string Alcance, int Capacidad);
