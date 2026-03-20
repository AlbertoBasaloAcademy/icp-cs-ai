# Registro en consola (requests/responses/errors)

Esta guía describe la implementación mínima para registrar en consola solicitudes HTTP, respuestas y excepciones no controladas usando solo funcionalidades nativas de ASP.NET Core.

Qué incluye:

- Proveedor de logging de consola activado en `Program.cs`.
- Middleware que registra:
  - Solicitud entrante: método, ruta y timestamp.
  - Respuesta: método, ruta, código de estado y duración en ms.
  - Excepciones no controladas: `LogError` con excepción y respuesta JSON genérica 500.

Cómo funciona:

- El middleware escribe un `LogInformation` al recibir la solicitud.
- Al completar la respuesta registra `LogInformation` o `LogWarning` para 4xx/5xx.
- En caso de excepción, registra `LogError` y devuelve un JSON con `titulo` y `detalle` genérico.

Archivo modificado: `AstroBookings/Program.cs`.

Ejemplo de uso:

1. Ejecutar la API:

```powershell
dotnet run --project .\AstroBookings\AstroBookings.csproj
```

2. Hacer peticiones a `/health` y `/cohetes` y observar los logs en la consola.

Criterios cumplidos:

- Proveedor de logging de consola nativo activo.
- Registro de solicitudes y respuestas con la información mínima requerida.
- Manejo de excepciones no controladas con log de error y respuesta controlada.

