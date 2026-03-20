namespace AstroBookings.Rockets;

public static class MapeoCohete
{
    public static RespuestaCohete ARespuesta(this Cohete cohete) =>
        new(cohete.Id, cohete.Nombre, cohete.Alcance.AValorApi(), cohete.Capacidad);

    public static string AValorApi(this AlcanceCohete alcance) => alcance switch
    {
        AlcanceCohete.Suborbital => "suborbital",
        AlcanceCohete.Orbital => "orbital",
        AlcanceCohete.Luna => "moon",
        AlcanceCohete.Marte => "mars",
        _ => throw new ArgumentOutOfRangeException(nameof(alcance), alcance, null)
    };

    public static bool IntentarAnalizarAlcance(string valor, out AlcanceCohete alcance)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            alcance = default;
            return false;
        }

        switch (valor.Trim().ToLowerInvariant())
        {
            case "suborbital":
                alcance = AlcanceCohete.Suborbital;
                return true;
            case "orbital":
                alcance = AlcanceCohete.Orbital;
                return true;
            case "moon":
                alcance = AlcanceCohete.Luna;
                return true;
            case "mars":
                alcance = AlcanceCohete.Marte;
                return true;
            default:
                alcance = default;
                return false;
        }
    }
}
