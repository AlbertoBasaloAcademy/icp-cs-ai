# ICP cs AI

Curso de desarrollo con IA para ICP usando C#

[Repositorio del curso](https://github.com/AlbertoBasaloAcademy/icp-cs-ai)

## Docs

- Documentación del curso en [carpeta Docs](./docs)

## Demo

Versión C# de la API de AstroBookings desarrollada con AI-Driven Development

Basada en [resumen ejecutivo de AstroBookings](https://github.com/AlbertoBasaloLabs/astro-bookings/blob/main/README_ES)

## Rocket Management API

The AstroBookings API now includes rocket management endpoints:

- `GET /rockets/`
- `GET /rockets/{id}`
- `POST /rockets/`
- `PUT /rockets/{id}`
- `DELETE /rockets/{id}`

End-to-end HTTP scenarios for the rockets feature are available in `tests/e2e/rockets/`.

## Launches API

Se ha añadido la gestión de lanzamientos:

- `GET /lanzamientos/`
- `GET /lanzamientos/{id}`
- `POST /lanzamientos/` (crea lanzamiento en estado `programado`)
- `PUT /lanzamientos/{id}`
- `DELETE /lanzamientos/{id}`
- `PATCH /lanzamientos/{id}/estado` (cambiar estado: programado, confirmado, exitoso, suspendido, cancelado)

La especificación funcional y el plan están en `specs/lanzamientos-estados-rentabilidad.spec.md`.

---

- **Author**: [Alberto Basalo](https://albertobasalo.dev)
- **Ai Code Academy en Español**: [AI code Academy](https://aicode.academy)
- **Socials**:
  - [X](https://x.com/albertobasalo)
  - [LinkedIn](https://www.linkedin.com/in/albertobasalo/)
  - [GitHub](https://github.com/albertobasalo)
