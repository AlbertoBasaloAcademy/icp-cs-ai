# Changelog

All notable changes to this project will be documented in this file.

## [0.1.1] - 2026-03-20

### Added
- Registro en consola de solicitudes HTTP (método, ruta, timestamp) y respuestas (código de estado y duración) mediante middleware nativo de ASP.NET Core.
- Registro de excepciones no controladas con `LogError` y respuesta JSON genérica 500 sin exponer detalles sensibles.

### Changed
- Habilitado proveedor de logging de consola nativo y ajustes en `Program.cs` para centralizar comportamiento de logging.

## [0.1.0] - 2026-03-18

### Added
- Rocket management API endpoints for creating, listing, retrieving, updating, and deleting rockets.
- In-memory rocket repository and request validation for supported ranges and passenger capacity.
- End-to-end HTTP scenarios for the rockets feature in `tests/e2e/rockets/`.

### Changed
- AstroBookings project version updated to `0.1.0`.
- README updated with rocket API endpoint and e2e scenario information.

## [0.2.0] - 2026-03-20

### Added
- Gestión de lanzamientos (`/lanzamientos`) con estados y asientos: modelado de lanzamientos, validaciones, repositorio en memoria, máquina de estados y endpoints CRUD + cambio de estado.
- Reglas de negocio implementadas: fecha futura obligatoria, umbral mínimo de pasajeros, suspensión por baja demanda y cancelación técnica con motivo.
- Issue asociada: https://github.com/AlbertoBasaloAcademy/icp-cs-ai/issues/2

### Changed
- Integración de la nueva feature en la composición de la app (`Program.cs`) y registro en DI del repositorio de lanzamientos.
