namespace AstroBookings.Lanzamientos;

public static class MaquinaEstadoLanzamiento
{
    public static bool IntentarCambiar(
        Lanzamiento lanzamiento,
        EstadoLanzamiento estadoDestino,
        string? motivo,
        out Lanzamiento actualizado,
        out string conflicto)
    {
        if (EsTerminal(lanzamiento.Estado))
        {
            actualizado = default!;
            conflicto = "El lanzamiento ya esta en estado terminal y no admite cambios.";
            return false;
        }

        if (!EsTransicionPermitida(lanzamiento.Estado, estadoDestino))
        {
            actualizado = default!;
            conflicto = $"No se permite la transicion de {lanzamiento.Estado.AValorApi()} a {estadoDestino.AValorApi()}.";
            return false;
        }

        var motivoNormalizado = motivo?.Trim();

        if (estadoDestino is EstadoLanzamiento.Suspendido && lanzamiento.PasajerosConfirmados >= lanzamiento.UmbralMinimoPasajeros)
        {
            actualizado = default!;
            conflicto = "No se puede suspender: hay pasajeros suficientes para superar el umbral minimo.";
            return false;
        }

        if (estadoDestino is EstadoLanzamiento.Cancelado && string.IsNullOrWhiteSpace(motivoNormalizado))
        {
            actualizado = default!;
            conflicto = "La cancelacion tecnica requiere un motivo no vacio.";
            return false;
        }

        actualizado = lanzamiento with
        {
            Estado = estadoDestino,
            MotivoEstado = estadoDestino is EstadoLanzamiento.Suspendido or EstadoLanzamiento.Cancelado
                ? motivoNormalizado
                : null
        };

        conflicto = string.Empty;
        return true;
    }

    private static bool EsTerminal(EstadoLanzamiento estado) =>
        estado is EstadoLanzamiento.Suspendido or EstadoLanzamiento.Cancelado or EstadoLanzamiento.Exitoso;

    private static bool EsTransicionPermitida(EstadoLanzamiento origen, EstadoLanzamiento destino) => (origen, destino) switch
    {
        (EstadoLanzamiento.Programado, EstadoLanzamiento.Confirmado) => true,
        (EstadoLanzamiento.Confirmado, EstadoLanzamiento.Exitoso) => true,
        (EstadoLanzamiento.Programado, EstadoLanzamiento.Suspendido) => true,
        (EstadoLanzamiento.Confirmado, EstadoLanzamiento.Suspendido) => true,
        (EstadoLanzamiento.Programado, EstadoLanzamiento.Cancelado) => true,
        (EstadoLanzamiento.Confirmado, EstadoLanzamiento.Cancelado) => true,
        _ => false
    };
}
