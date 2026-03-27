# Agents Instructions

- **{Root_Folder}**: {Carpeta raíz para el proyecto y agentes.}
- **{Agents_Folder}**: {Carpeta para archivos relacionados a los agentes.}
- **{Agents_file}**: {Archivo de instrucciones principales para agentes.}
- **{Project_Folder}**: {Carpeta para archivos relacionados al proyecto.}

## Product Overview

{Nombre del producto} es un {breve descripción del producto}.

- {Características adicionales o clave del producto.}
  
## Technical Implementation

### Tech Stack

- **Language**: {lenguaje y versión}
- **Framework**: {framework y versión}
- **Database**: {base de datos}
- **Security**: {estrategia de seguridad}
- **Testing**: {framework de pruebas}
- **Logging**: {herramienta de registro}

### Development workflow

```bash
# Configurar el proyecto
{}
# Construir/compilar el proyecto
{}
# Ejecutar el proyecto
{}
# Probar el proyecto
{}
# Desplegar el proyecto
```

### Folder structure
```text
.                         # Project root  
├── {Agents_file}         # Este archivo con instrucciones para agentes de IA
├── {Agents_Folder}/      # Archivos relacionados a agentes (skills, specs, etc)
│   ├── agents/           # Definiciones específicas de agentes
|   ├── prompts/          # Directorio de prompts reutilizables
|   └── skills/           # Habilidades personalizadas de agentes
├── {Project_Folder}/     # Archivos relacionados al proyecto (specs, planes, etc)
|   └── specs/            # Especificaciones y planes
├── CHANGELOG.md          # Historial del proyecto y actualizaciones
├── README.md             # Resumen del proyecto amigable para humanos
├── src/                  # Carpeta de código fuente
├── tests/                # Archivos de pruebas
└── other_files/          # Otros archivos y carpetas relevantes 
```

## Environment
- **OS dev**: `Windows` | `Linux` | `MacOS`
- **Terminal**: ` cmd` | `PowerShell` | `bash` | `zsh`
- **Git remote**: {URL remota de git}
- **Default branch**: `main` | `master` 

## Behavior Guidelines

- El código y la documentación deben estar en inglés.
- Las respuestas del chat deben estar en el idioma del prompt de usuario.
- Sacrifica la gramática por concisión cuando sea necesario para ajustarte a los límites de respuesta.
- Cuando uses plantillas, asegúrate de reemplazar los {placeholders} con valores reales.

### Naming Conventions

Usa slugs con guiones para cualquier identificador o nombres de archivo que no sean de código.

Prefija las especificaciones, ramas y mensajes de commit con las siguientes etiquetas:

- `feat` : Para nuevas funcionalidades o cambios significativos.
- `fix` : Para correcciones de errores o mejoras menores.
- `chore` : Para tareas de rutina, mantenimiento o cambios no funcionales.
