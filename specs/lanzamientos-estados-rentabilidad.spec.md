# Lanzamientos: estados, asientos y rentabilidad

## Trazabilidad
- Issue GitHub: https://github.com/AlbertoBasaloAcademy/icp-cs-ai/issues/2

## Descripción del Problema
- Como operador de vuelos, quiero programar lanzamientos para un cohete en una fecha futura para organizar la operación.
- Como responsable comercial, quiero definir asientos con precio por categoría para vender plazas con reglas claras.
- Como responsable financiero, quiero establecer un umbral mínimo de pasajeros para considerar rentable un lanzamiento.
- Como operador, quiero que el lanzamiento avance por estados controlados para reflejar el ciclo real: `programado -> confirmado -> exitoso`.
- Como operador, quiero poder marcar un lanzamiento como `suspendido` por baja demanda o `cancelado` por razones técnicas.

## Solución Propuesta
- Añadir una nueva capacidad de gestión de lanzamientos vinculados a un cohete existente.
- Modelar cada lanzamiento con:
  - Identificador.
  - Identificador del cohete.
  - Fecha y hora programada (futura).
  - Estado del lanzamiento.
  - Configuración de asientos por tipo/categoría con su precio.
  - Umbral mínimo de pasajeros para rentabilidad.
  - Pasajeros confirmados (contador o colección simplificada para esta fase).
- Definir reglas de transición de estado para impedir cambios inválidos.
- Definir reglas de suspensión y cancelación con motivo para auditoría funcional.
- Exponer endpoints REST consistentes con el estilo actual de la API Minimal.

## Reglas de Dominio
1. La fecha de un lanzamiento debe estar en el futuro al momento de crearlo.
2. Un lanzamiento debe pertenecer a un cohete existente.
3. El umbral mínimo de pasajeros debe ser mayor que 0.
4. Cada tipo de asiento debe tener precio mayor que 0.
5. El estado inicial de un lanzamiento es `programado`.
6. Transiciones permitidas:
   - `programado -> confirmado`
   - `confirmado -> exitoso`
   - `programado -> suspendido`
   - `confirmado -> suspendido`
   - `programado -> cancelado`
   - `confirmado -> cancelado`
7. `suspendido`, `cancelado` y `exitoso` son estados terminales (no permiten nuevas transiciones).
8. La suspensión por baja demanda aplica cuando pasajeros confirmados es menor que el umbral mínimo.
9. La cancelación técnica requiere motivo técnico no vacío.

## Criterios de Aceptación
- [ ] Cuando se crea un lanzamiento con cohete existente, fecha futura, asientos válidos y umbral válido, el sistema deberá devolver `201 Created` con los datos del lanzamiento en estado `programado`.
- [ ] Si el cohete indicado no existe, el sistema deberá rechazar la creación del lanzamiento con `404 Not Found`.
- [ ] Si la fecha indicada no es futura, el sistema deberá rechazar la solicitud con `400 Bad Request` informando el campo inválido.
- [ ] Si el umbral mínimo de pasajeros es menor o igual que 0, el sistema deberá rechazar la solicitud con `400 Bad Request`.
- [ ] Si un tipo de asiento tiene precio menor o igual que 0, el sistema deberá rechazar la solicitud con `400 Bad Request`.
- [ ] Cuando se solicita avanzar de `programado` a `confirmado`, el sistema deberá aplicar la transición y devolver el lanzamiento actualizado.
- [ ] Cuando se solicita avanzar de `confirmado` a `exitoso`, el sistema deberá aplicar la transición y devolver el lanzamiento actualizado.
- [ ] Si se solicita una transición de estado no permitida, el sistema deberá responder con `409 Conflict`.
- [ ] Cuando se solicita suspensión y los pasajeros confirmados son menores que el umbral, el sistema deberá establecer estado `suspendido` y registrar motivo.
- [ ] Si se solicita suspensión por baja demanda sin cumplir la condición de umbral, el sistema deberá rechazar la operación con `409 Conflict`.
- [ ] Cuando se solicita cancelación por razón técnica con motivo no vacío y estado no terminal, el sistema deberá establecer estado `cancelado`.
- [ ] Si se intenta cambiar el estado de un lanzamiento `suspendido`, `cancelado` o `exitoso`, el sistema deberá rechazar la operación con `409 Conflict`.
- [ ] Cuando se consulte un lanzamiento existente, el sistema deberá incluir estado, umbral mínimo, pasajeros confirmados y asientos configurados.
- [ ] Cuando se liste lanzamientos, el sistema deberá devolver todos los registros creados preservando su estado actual.

## Alcance Técnico Inicial
- Persistencia en memoria con `Dictionary`, alineada al diseño actual.
- Endpoints bajo ruta base `/lanzamientos`.
- Contratos y validaciones en español, alineados al repositorio.
- Pruebas HTTP en `tests/e2e` para flujo nominal y errores de transición.

## Fuera de Alcance
- Reserva completa de asientos por pasajero con inventario detallado por plaza.
- Pagos, reembolsos o facturación.
- Reprogramación automática de lanzamientos suspendidos.
- Persistencia en base de datos.

## Plan de Implementación
1. Diseñar modelo de dominio de lanzamiento
- Definir entidades y DTOs de lanzamiento, estado y asiento.
- Definir enum de estados con terminales explícitos.
- Decidir representación de pasajeros confirmados para esta iteración.

2. Extender repositorio en memoria
- Crear interfaz de repositorio de lanzamientos.
- Implementar almacenamiento con `Dictionary<int, Lanzamiento>`.
- Resolver relación con cohetes existentes por `coheteId`.

3. Implementar validaciones de entrada
- Validar fecha futura, umbral y precios.
- Validar existencia de cohete antes de crear.
- Estandarizar respuestas `ValidationProblem` para campos inválidos.

4. Implementar máquina de estados
- Centralizar transiciones permitidas en un componente de dominio.
- Validar reglas de suspensión por umbral.
- Validar cancelación técnica con motivo obligatorio.

5. Exponer endpoints Minimal API
- Mapear CRUD básico de lanzamientos.
- Añadir endpoint(s) de cambio de estado.
- Devolver códigos HTTP esperados (`201`, `400`, `404`, `409`).

6. Integrar en composición de la app
- Registrar repositorio de lanzamientos en DI.
- Mapear endpoints de lanzamientos en `Program.cs`.
- Mantener convenciones de nombres y estructura por feature folder.

7. Añadir pruebas HTTP end-to-end
- Crear escenarios felices de creación y transición completa.
- Crear escenarios de error por transición inválida.
- Crear escenarios de suspensión/cancelación con reglas de negocio.

8. Actualizar documentación funcional
- Documentar estados, reglas y ejemplos de payload.
- Documentar códigos de error y conflictos de estado.
- Enlazar documentación con la spec y el issue.
