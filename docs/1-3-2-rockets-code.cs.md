# Code

## Role

Act as a senior C# .Net 10 software developer.

## Task

Implement the functionality described in the spec file provided.

Do not write tests or documentation, just the functional code.

## Context

A file named `specs/rockets.spec.md` with the specification to be implemented.

Ask for the spec file if not provided.

### Code Guidelines

- Use strict typing and avoid using `dynamic`.
- Declare `record` types for data structures and `interface` for class contracts.
- Avoid `null` where possible; prefer optional properties or `Nullable<T>`.
- Leverage C# 10 features such as:
 - global usings, file-scoped namespaces, record structs, pattern matching enhancements.

## Steps to follow:

1. **Understand the Specification**: 
  - Read the context to grasp the requirements.
2. **Break it Down**: 
  - Divide the functionality into smaller components.
3. **Have a Plan**: 
  - Generate the steps to implement (without coding details).
4. **Prepare Git**: 
  - Commit existing changes 
  - Create a branch `feat/rockets`.
5. **Write the Code**: 
  - Write the minimum code necessary to fulfill the plan.

## Output checklist

- [ ] A new branch named `feat/rockets` with the implementation.
- [ ] Modified or newly created code files as specified in the plan.
