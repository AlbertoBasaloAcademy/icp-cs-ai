# Instrucciones para Agentes

## Resumen del producto
- AstroBookings es una API web en .NET para gestionar cohetes.
- El backend expone `/health` y un CRUD bajo `/cohetes`.
- La persistencia es en memoria; al reiniciar la app se pierde el estado.

## Comandos del repositorio

### Build y ejecución
```powershell
Set-Location .\AstroBookings
dotnet restore .\AstroBookings.csproj
dotnet build .\AstroBookings.csproj -q
dotnet run --project .\AstroBookings.csproj --launch-profile http
```

### Tests disponibles
```powershell
Set-Location .\AstroBookings
dotnet test .\AstroBookings.csproj
```

- No hay un proyecto de tests automatizados separado; `dotnet test` solo recompila el proyecto web.
- Las pruebas reales del repo son escenarios HTTP `.http`.

### Ejecutar un escenario individual
- Inicia la API con `dotnet run` y ejecuta un archivo `.http` con el cliente REST del editor.
- El flujo activo está en `tests\e2e\cohetes\`.
- Para validar una sola request, usa por ejemplo `tests\e2e\cohetes\03-validation.http`.
- Smoke tests rápidos: `AstroBookings\SmokeTests\health.http` y `AstroBookings\SmokeTests\openapi.http`.

## Arquitectura de alto nivel
- `AstroBookings\Program.cs` compone toda la app: registra OpenAPI, inyecta `IRepositorioCohete` como singleton, activa HTTPS, publica `/health` y delega el CRUD a `MapearEndpointsCohete()`.
- La feature vive en `AstroBookings\Rockets\` y está separada por responsabilidad:
  - `EndpointsCohete.cs`: definición del grupo `/cohetes` y handlers Minimal API.
  - `IRepositorioCohete.cs` + `RepositorioCoheteEnMemoria.cs`: contrato y almacenamiento en `Dictionary<int, Cohete>`.
  - `ModelosCohete.cs`: records de dominio, borrador, request y response.
  - `ValidacionCohete.cs`: validación de entrada y construcción del `BorradorCohete`.
  - `MapeoCohete.cs`: mapeo entre `AlcanceCohete` y los strings expuestos por la API.
- Las pruebas end-to-end no llaman helpers internos; verifican la API arrancada mediante archivos `.http`.
- `Properties\launchSettings.json` fija los puertos de desarrollo: `http://localhost:5070` y `https://localhost:7094`.

## Convenciones clave del código
- El código activo está en español: nombres como `IRepositorioCohete`, `MapearEndpointsCohete`, `SolicitudCohete` y rutas `/cohetes`.
- En `Rockets\` existen archivos equivalentes en inglés (`Rocket*.cs`, `IRocketRepository.cs`, `InMemoryRocketRepository.cs`), pero actualmente están vacíos y no están conectados desde `Program.cs`. Trabaja sobre los archivos en español salvo que el cambio pida completar la versión en inglés.
- La API usa Minimal API con métodos privados estáticos dentro de la clase de endpoints, no controladores MVC.
- El repositorio se registra como singleton; cualquier cambio de estado durante la ejecución queda compartido entre requests hasta reiniciar la app.
- La entrada HTTP usa `SolicitudCohete` con `nombre`, `alcance` y `capacidad`. La salida usa `RespuestaCohete`.
- El enum interno `AlcanceCohete` se expone como strings `"suborbital"`, `"orbital"`, `"moon"` y `"mars"`.
- La validación devuelve `Results.ValidationProblem(...)` con un diccionario de errores por campo. Reutiliza `ValidacionCohete.IntentarValidar(...)` antes de añadir lógica nueva de creación o actualización.
- Los handlers de lectura y actualización siguen el patrón `Intentar...` del repositorio en vez de lanzar excepciones para el flujo normal de `404`.
- OpenAPI solo se publica en desarrollo mediante `app.MapOpenApi()`. Verifica `/openapi/v1.json` cuando cambies contratos HTTP.
- Los ejemplos y documentación del repositorio mezclan inglés y español, pero la implementación conectada hoy usa español como fuente de verdad.

## Referencias útiles del repo
- `README.md`: visión general del curso y de la demo.
- `docs\2-1-1-crear-instrucciones.md`: intención original de esta guía.
- `tests\e2e\cohetes\`: escenarios CRUD y validación de la API activa.
