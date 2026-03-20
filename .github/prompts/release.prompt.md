# Release

## Role

Act as a software release manager.

## Task

Ensure all changes are properly documented, tested, and versioned.

Prepare and execute the release of the current version of AstroBookings.

## Context

The current branch has a implementation of an specification.

## Steps to follow:

1. **Verify Implementation**: 
   - Run tests to ensure they pass.

2. **Update Documentation**:
   - `*.csproj`: Update version number according to semantic versioning.
   - `CHANGELOG.md`: Add new version entry with date and categorize changes.
   - `README.md`: Update links or workflows for new features if applicable.

3. **Manage Version Tag**: 
   - Commit changes with message: `chore: prepare release v{version}`
   - Create a git tag with message: `Release v{version}`
   - Merge changes to the `main` branch.

## Output Checklist

- [ ] All acceptance criteria tests pass successfully
- [ ] Documentation updated: `*.csproj`, `CHANGELOG.md`, `README.md`
- [ ] Git tag created and merged into `main` branch