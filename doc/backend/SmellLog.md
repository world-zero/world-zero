# Smell Log

For a variety of reasons (most of which are caused by EF Core being obnoxious),
here is the list of known smells.

## `IEntity.Id` not being an `ISingleValueObject`

Ideally, `IEntity` would be generic on an `ISingleValueObject`, but I cannot
seem to get EF Core to cooperate, even if the property only has one meaningful
value. As a result, `IEntity<T>` is extended to `IIdEntity`, which uses an int
property wrapper around the `ISingleValueObject`, and `INamedEntity`, which
uses a string property.

For a similar reason, `IEntityMap` does not use the keys of the two entities as
its ID.

## Navigation Properties on `IEntity` Implementations

These are just so EF Core can more easily cooperate. They aren't a smell so
much as a slight tinge since these have been made internal to `Common/` and
`Data/` and testing.
