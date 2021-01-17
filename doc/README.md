# Codebase Notes

## General Notes

`Common/`, `Data/`, and `Service/` are written in .NET Standard - these layers
do not build any executables, those will be left for the ports and adapters.  

Value Objects are tightly coupled. Entities are tightly coupled between one
another as well.  

If performance becomes an issue, the entity DTOs `GetHashCode` overrides
perform a lot of multiplication.

## Architecture, Projects, and Solutions

This project uses two architectures for the back end: Layered, and Hexagonal.

### Layered Architecture

Layered Architecture in this setting doesn't really have a business layer due
to the simplicity of the business objects - the alternative to having entities
that use value objects would be to have struct-like entity classes that are
mementos for their corresponding snapshot business object (which are very
anemic in this application), and that it would become necessary to write
caretakers to use repos to return snapshot classes, and these caretakers would
then be used by the service classes. Honestly, that was an extreme overkill
for the project, and it violated KISS much more than how the current
architecture slightly breaks separation of concerns. As a result, the entities
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
- `Service/` references `Common/` and `Data/` and is referenced by `Port`.
- `Port/` references `Common` and `Service/`.

Additionally, the test projects reference things as relevant, obviously.

## Responsibilities

Now I don't know how universal these responsibilities are, so I am stating them
here because explicitness can only be helpful and it will help me remember
them.

1. Entities are in charge of being either valid or uninitialized, defaulted to
certain values if appropriate.

    - An example of this would be how `Character.VotePointsLeft` defaulting to 100.
    - Another example is how all entities with an int ID will default to 0,
    signifying that they are unset.

2. Repositories are in charge of making sure that the collection of entities have
their rules enforced. Repositories will set the ID of a entity (if it is an int)
on `IEntityRepo<T>.Save()`. Again, since a valid but uninitialized ID is 0,
repos start counting meaningful IDs at 1.

    - An example of this would be ensuring that `Faction.Name`s are unique.  

That all said, there is a slight addendum to how entity repositories work. For
more, check out `backend/EntitesAndRepos.md`.

3. Services are responsible for passing inputs to repos, dealing with any
responses, performing unit-of-works (which are responsible for enforcing
inter-entity rules), and performing any other business functions an entity
would be capable of doing. Additionally, they are in charge of ensuring that
relational entity repos are correct, i.e. ensuring that an ID in a many-to-many
tuple is valid. This is not done by the repo as this would require exceeding
the bounds of the repo by accessing another repo, which is firmly in the
grounds of a service class.

    - An example of this would be a unit of work that makes sure that a change
    in a `Chacater.Faction` will also remove them from the old faction's member
    list (if not null) and add them to the new faction's member list (if not
    null).

## Exceptions

*I would describe these as a lil bit too exception-happy and restrictive and I
am sorry.*

Most, if not all, properties will throw `ArgumentException`s on a sad or bad
path, excluding the `ArgumentNullException` usage. Most, if not all, other
classes will follow suit. Full disclosure, I regret the degree at which
everything throws exceptions as opposed to returning, say, an empty list;
fortunately, there are not many cases of this.  

`ArgumentException` is thrown when the user of some code does something bad.
`InvalidOperationException` is thrown when an unreachable case is reached,
meaning a bug has been found.

## Style Conventions

First and foremost, this is an open-source and fan-built project. Very few
people are going to be spending lots of time really digging into this code. As
a result, it is ***extremely*** recommended to err on the side of descriptive
and possibly long names for anything not a local variable in a function.
Additionally, avoid abbreviating function names. This way, we can ensure very
high readability for all contributers to this project, regardless of their
knowledge of the project.

When in doubt, default to the suggested conventions by Microsoft. That said,
this codebase does deviate from a few of these guidelines, and I want to lay
out some explicit rules here.

- Per the docs, interfaces should begin with `I`; for this project, abstract
classes should begin with `ABC`.
- Non-public fields and properties are _camelCase, and only private function
members are _camelCase.
- Do not exceed 79 characters on a single line, unless the exceeding line
throws an exception and the message breaks this rule.
- When breaking &&-ed or ||-ed conditions, please follow the Microsoft
guideline of wrapping each sub-condition in parentheses. Additionally, assuming
that these conditions are going to need to be broken between lines, please
start the next line with the condition operator to increase readability.
