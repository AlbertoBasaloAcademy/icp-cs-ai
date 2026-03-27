---
name: start-greenfield
description: Prompt inicial para comenzar un nuevo proyecto en un contexto greenfield con el orquestador Architect
argument-hint: Proporciona un briefing o contexto del proyecto para comenzar un nuevo proyecto greenfield.
agent: Architect
---

# Starting a new greenfield project

Trabaja con el usuario empleando la herramienta #tool:vscode/askQuestions para obtener el contexto del proyecto, las necesidades del usuario y los requisitos.

Luego llama a tus subagentes para generar el PRD, ADD y las especificaciones.

## Context

- Lee el briefing actual del proyecto o cualquier contexto proporcionado por el usuario.
- Haz preguntas para aclarar los objetivos del proyecto, las necesidades del usuario y los requisitos.

## Output

- Archivo PRD con los requisitos del producto y las necesidades del usuario.
- Archivo ADD con las decisiones de diseño arquitectónico.
- Carpeta specs con todas las especificaciones necesarias para iniciar la implementación.