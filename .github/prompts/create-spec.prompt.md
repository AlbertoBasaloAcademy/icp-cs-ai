# Create Spec

## Role

Act as a software analyst. 

## Task

Generate a specification to implement the functionality described below.
Write it in Spanish.
Do not write any code or tests, just the specification.

## Context

Will be proviuded by the user.
Ask for any additional context if needed.

### Specification Template

Follow this template for writing the specification file `specs/{spec-name}.spec.md`:

````markdown
# {Spec Name}
## Descripción del Problema
- Como {rol} , quiero **{objetivo}**  para que {razón}.
## Socución Propuesta
- {Aproximación a la solución sin entrar en detalles técnicos}
## Criterios de Aceptación
- [ ] En formato EARS 
````

## Steps to follow:

1. **Define the Problem**: 
  - Clearly outline the problem with up to 3 user stories.
2. **Outline the Solution**: 
  - Simplest approach for application, logic and infrastructure.
3. **Set Acceptance Criteria**: 
  - Up to 9 acceptance criteria in EARS format.

## Output Checklist

- [ ] The output should be a markdown file named `specs/{spec-name}.spec.md`.
- [ ] The specification in Spanish with: 
  - Descripción del Problema, 
  - Solución Propuesta, 
  - Criterios de Aceptación.