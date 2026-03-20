namespace AstroBookings.Lanzamientos;

public static class MapeoLanzamiento
{
    public static RespuestaLanzamiento ARespuesta(this Lanzamiento lanzamiento) =>
        new(
            lanzamiento.Id,
            lanzamiento.CoheteId,
            lanzamiento.FechaProgramada,
            lanzamiento.Estado.AValorApi(),
            lanzamiento.UmbralMinimoPasajeros,
            lanzamiento.PasajerosConfirmados,
            lanzamiento.Asientos.Select(asiento => new RespuestaAsientoLanzamiento(asiento.Tipo, asiento.Precio)).ToArray(),
            lanzamiento.MotivoEstado);

    public static string AValorApi(this EstadoLanzamiento estado) => estado switch
    {
        EstadoLanzamiento.Programado => "programado",
        EstadoLanzamiento.Confirmado => "confirmado",
        EstadoLanzamiento.Exitoso => "exitoso",
        EstadoLanzamiento.Suspendido => "suspendido",
        EstadoLanzamiento.Cancelado => "cancelado",
        _ => throw new ArgumentOutOfRangeException(nameof(estado), estado, null)
    };

    public static bool IntentarAnalizarEstado(string valor, out EstadoLanzamiento estado)
    {
        if (string.IsNullOrWhiteSpace(valor))
        {
            estado = default;
            return false;
        }

        switch (valor.Trim().ToLowerInvariant())
        {
            case "programado":
                estado = EstadoLanzamiento.Programado;
                return true;
            case "confirmado":
                estado = EstadoLanzamiento.Confirmado;
                return true;
            case "exitoso":
                estado = EstadoLanzamiento.Exitoso;
                return true;
            case "suspendido":
                estado = EstadoLanzamiento.Suspendido;
                return true;
            case "cancelado":
                estado = EstadoLanzamiento.Cancelado;
                return true;
            default:
                estado = default;
                return false;
        }
    }
}
