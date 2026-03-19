namespace AstroBookings.Rockets;

public static class ValidacionCohete
{
    public static bool IntentarValidar(SolicitudCohete solicitud, out BorradorCohete cohete, out Dictionary<string, string[]> errores)
    {
        errores = new Dictionary<string, string[]>();

        var nombre = solicitud.Nombre?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(nombre))
        {
            errores[nameof(solicitud.Nombre)] = ["Name is required."];
        }

        var valorAlcance = solicitud.Alcance?.Trim() ?? string.Empty;
        if (!MapeoCohete.IntentarAnalizarAlcance(valorAlcance, out var alcance))
        {
            errores[nameof(solicitud.Alcance)] = ["Range must be one of: suborbital, orbital, moon, mars."];
        }

        if (solicitud.Capacidad is < 1 or > 10)
        {
            errores[nameof(solicitud.Capacidad)] = ["Capacity must be between 1 and 10."];
        }

        if (errores.Count > 0)
        {
            cohete = default!;
            return false;
        }

        cohete = new BorradorCohete(nombre, alcance, solicitud.Capacidad);
        return true;
    }
}
