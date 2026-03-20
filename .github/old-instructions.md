# Instrucciones para Agentes

## Resumen del Producto
- AstroBookings es una API Web en .NET para gestión de cohetes.
- Expone endpoints de salud y CRUD bajo `/health` y `/cohetes`.
- Usa almacenamiento en memoria para escenarios de demo y aprendizaje.

## Implementación Técnica

### Stack Tecnológico
- Lenguaje: **C# 14 sobre .NET 10 (net10.0)**
- Framework: **ASP.NET Core Minimal API 10**
- Base de datos: **Sin BD; repositorio en memoria con `Dictionary`**
- Seguridad: **HTTPS, validación de entrada y sin autenticación activa**
- Tests: **Pruebas HTTP con archivos `.http` (SmokeTests y `tests/e2e`)**
- Logging: **Logging nativo de ASP.NET Core configurado en `appsettings`**

### Flujo de desarrollo

```powershell
# Configurar el proyecto
dotnet restore .\AstroBookings\AstroBookings.csproj
# Construir/Compilar el proyecto
dotnet build .\AstroBookings\AstroBookings.csproj
# Ejecutar el proyecto
dotnet run --project .\AstroBookings\AstroBookings.csproj
# Ejecutar tests
dotnet test .\AstroBookings\AstroBookings.csproj
# Desplegar el proyecto
dotnet publish .\AstroBookings\AstroBookings.csproj -c Release
```

### Estructura de carpetas

```text
.                                        # Raíz del proyecto
├── .github/copilot-instructions.md      # Archivo con instrucciones para agentes
├── README.md                            # Documentación principal
├── AstroBookings/                       # API ASP.NET Core
│   ├── Program.cs                       # Composición de la app y endpoints base
│   ├── Cohetes/                         # Funcionalidad de cohetes
│   └── SmokeTests/                      # Pruebas rápidas HTTP
├── tests/e2e/                           # Escenarios HTTP end-to-end
├── docs/                                # Guías y material del curso
└── .github/prompts/                     # Prompts de trabajo del repositorio
```

## Entorno
- Las variables y procedimientos del código y la documentación deben estar en Español.
- Priorizar concisión sobre gramática en las respuestas.
- Entorno Windows usando terminal PowerShell.
- La rama por defecto es `main`.
