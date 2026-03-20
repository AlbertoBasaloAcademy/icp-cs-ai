# Registro de llamadas, respuestas y errores en consola
## Descripción del Problema
- Como desarrollador de la API, quiero **ver en consola cada llamada HTTP recibida** para poder observar el uso real del servicio durante ejecución local.
- Como desarrollador de la API, quiero **ver en consola el resultado de cada respuesta HTTP** para poder diagnosticar rápidamente códigos de estado inesperados.
- Como responsable técnico, quiero **registrar errores no controlados en consola** para poder identificar fallos sin agregar librerías externas.

## Solución Propuesta
- Incorporar una solución mínima con capacidades nativas de ASP.NET Core para registrar en consola solicitudes, respuestas y excepciones.
- Centralizar el comportamiento en la configuración de la aplicación y en el pipeline HTTP existente, evitando dependencias de terceros.
- Mantener el formato de salida simple y consistente para facilitar lectura en desarrollo y pruebas manuales.

## Criterios de Aceptación
- [ ] Cuando la aplicación inicie, el sistema deberá tener habilitado el proveedor de logging de consola usando únicamente funcionalidades nativas de .NET.
- [ ] Cuando llegue una solicitud HTTP a cualquier endpoint, el sistema deberá registrar en consola al menos método, ruta y marca de tiempo.
- [ ] Cuando la aplicación complete una respuesta HTTP, el sistema deberá registrar en consola al menos método, ruta, código de estado y duración de procesamiento.
- [ ] Si durante el procesamiento de una solicitud ocurre una excepción no controlada, entonces el sistema deberá registrar en consola el error con nivel de severidad de error.
- [ ] Si ocurre una excepción no controlada, entonces el sistema deberá devolver una respuesta de error controlada sin exponer detalles sensibles internos.
- [ ] Cuando una solicitud termine correctamente sin errores, el sistema deberá registrar el evento con un nivel informativo.
- [ ] Si una solicitud produce un código de estado de cliente o servidor (4xx o 5xx), entonces el sistema deberá reflejarlo explícitamente en el registro de respuesta.
- [ ] Mientras la aplicación esté en ejecución local, el sistema deberá mantener activos los registros de llamadas, respuestas y errores sin requerir librerías externas ni configuración adicional fuera del proyecto.
