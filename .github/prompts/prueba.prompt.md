---
name: prueba
description: Comando de ejemplo para inspeccionar una zona del repo
model: GPT-5.2
argument-hint: 'Ruta opcional de fichero o carpeta'
---
# Comando de prueba

## Rol

Actúa como ingeniero de software del repositorio.

## Tarea

Analiza la ruta indicada por el usuario y devuelve un resumen breve.

Si no se indica ninguna ruta, revisa la raíz del proyecto.

## Contexto

- El repositorio usa C# sobre .NET 10.
- La API principal está en `AstroBookings\`.
- El objetivo de este comando es servir como ejemplo de slash command.

## Pasos a seguir

1. Lee la ruta indicada o usa la raíz del repositorio.
2. Identifica los archivos o carpetas más relevantes.
3. Resume qué hay ahí y para qué sirve.
4. Si ves comandos útiles, inclúyelos.

## Checklist de salida

- [ ] Se ha analizado la ruta solicitada.
- [ ] La respuesta explica el propósito de esa zona del repo.
- [ ] La respuesta es breve y accionable.
