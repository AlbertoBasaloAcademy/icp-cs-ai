namespace AstroBookings.Lanzamientos;

public static class ValidacionLanzamiento
{
    public static bool IntentarValidarSolicitud(
        SolicitudLanzamiento solicitud,
        out BorradorLanzamiento borrador,
        out Dictionary<string, string[]> errores)
    {
        errores = new Dictionary<string, string[]>();

        if (solicitud.CoheteId <= 0)
        {
            errores[nameof(solicitud.CoheteId)] = ["El coheteId debe ser mayor que 0."];
        }

        if (solicitud.FechaProgramada <= DateTimeOffset.UtcNow)
        {
            errores[nameof(solicitud.FechaProgramada)] = ["La fecha programada debe estar en el futuro."];
        }

        if (solicitud.UmbralMinimoPasajeros <= 0)
        {
            errores[nameof(solicitud.UmbralMinimoPasajeros)] = ["El umbral minimo de pasajeros debe ser mayor que 0."];
        }

        if (solicitud.PasajerosConfirmados < 0)
        {
            errores[nameof(solicitud.PasajerosConfirmados)] = ["Los pasajeros confirmados no pueden ser negativos."];
        }

        var asientosValidados = new List<AsientoLanzamiento>();
        if (solicitud.Asientos is null || solicitud.Asientos.Count == 0)
        {
            errores[nameof(solicitud.Asientos)] = ["Debe incluir al menos un asiento con precio."];
        }
        else
        {
            for (var i = 0; i < solicitud.Asientos.Count; i++)
            {
                var asiento = solicitud.Asientos[i];
                var tipo = asiento.Tipo?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(tipo))
                {
                    errores[$"{nameof(solicitud.Asientos)}[{i}].{nameof(asiento.Tipo)}"] = ["El tipo de asiento es obligatorio."];
                }

                if (asiento.Precio <= 0)
                {
                    errores[$"{nameof(solicitud.Asientos)}[{i}].{nameof(asiento.Precio)}"] = ["El precio del asiento debe ser mayor que 0."];
                }

                if (!string.IsNullOrWhiteSpace(tipo) && asiento.Precio > 0)
                {
                    asientosValidados.Add(new AsientoLanzamiento(tipo, asiento.Precio));
                }
            }
        }

        if (errores.Count > 0)
        {
            borrador = default!;
            return false;
        }

        borrador = new BorradorLanzamiento(
            solicitud.CoheteId,
            solicitud.FechaProgramada,
            solicitud.UmbralMinimoPasajeros,
            solicitud.PasajerosConfirmados,
            asientosValidados);

        return true;
    }

    public static bool IntentarValidarCambioEstado(
        SolicitudCambioEstadoLanzamiento solicitud,
        out EstadoLanzamiento estadoDestino,
        out string? motivo,
        out Dictionary<string, string[]> errores)
    {
        errores = new Dictionary<string, string[]>();

        if (!MapeoLanzamiento.IntentarAnalizarEstado(solicitud.Estado ?? string.Empty, out estadoDestino))
        {
            errores[nameof(solicitud.Estado)] = ["El estado debe ser uno de: programado, confirmado, exitoso, suspendido, cancelado."];
        }

        motivo = solicitud.Motivo?.Trim();

        if (errores.Count > 0)
        {
            estadoDestino = default;
            return false;
        }

        return true;
    }
}
