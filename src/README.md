# Codebase Notes

## General Notes

Common/ is written in .NET Standard, and Data/ is written in .NET Core since
EF Core requires .NET Core and I need EFC for DbContext - as a result, every
layer depending on Data/ has to be in .NET Core too. That said, these layers do
not build any executables, those will be left for the ports and adapters.  

If performance becomes an issue, the back end persistence is handled by
EntityFramework Core (EF Core), which is not particularly efficient - but it is
extremely time saving for development, especially this early on. An efficient
alternative to this would be Dapper with Dapper.Contrib.

## Architecture, Projects, and Solutions

This project uses two architectures for the back end: Layered, and Hexagonal.

### Layered Architecture

Layered Architecture in this setting doesn't really have a business layer due
to the simplicity of the business objects - the alternative to having models
that use value objects would be to have struct-like model classes that are
mementos for their corresponding snapshot business object (which are very
anemic in this application), and that it would become necessary to write
caretakers to use repos to return snapshot classes, and these caretakers would
then be used by the service classes. Honestly, that was an extreme overkill
for the project, and it violated KISS much more than how the current
architecture slightly breaks separation of concerns. As a result, the models
are just the value object properties of the business classes, and any function
members on those business classes have been moved to the corresponding service
class.

### Hexagonal

No changes to this architecture are made.

### Projects and Solutions

The solution `WorldZero.sln` is the only solution for this project.

This application uses Layered architecture with Hexagonal architecture.

- `Common/` references nothing and is referenced by everything.
- `Data/` references nothing except `Common/` and is referenced by `Service/`.
- `Service/` references `Common/` and `Data/` and is referenced by nothing.

Additionally, the test projects reference things as relevant, obviously.
Similarly, `Tools/` references all of the source projects, and the needed test
projects reference this as well, as it contains testing injections for things
like in memory repo implementations.

## Responsibilities

Now I don't know how universal these responsibilities are, so I am stating them
here because explicitness can only be helpful and it will help me remember
them.

1. Models are in charge of being either valid or uninitialized, defaulted to
certain values if appropriate.
    - An example of this would be how `Character.VotePointsLeft` defaulting to
100).
    - Another is example is how all models with an int ID will default to 0,
    signifying that they are unset.
2. Repositories are in charge of making sure that the collection of models have
their rules enforced. Repositories will set the ID of a model (if it is an int)
on `IModelRepo<T>.Save()`. Again, since a valid but uninitialized ID is 0,
repos start counting meaningful IDs at 1.
    - An example of this would be ensuring that `Faction.FactionName`s are
    unique.
3. Services are responsible for passing inputs to repos, dealing with any
responses, performing unit-of-works (which are responsible for enforcing
inter-model rules), and performing any other business functions an entity
would be capable of doing.
    - An example of this would be a unit of work that makes sure that a change
    in a `Chacater.Faction` will also remove them from the old faction's member
    list (if not null) and add them to the new faction's member list (if not
    null).
