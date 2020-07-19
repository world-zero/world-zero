# Entities

Currently, these exist to be data entities that use EF Core's data annotations to
handle almost all of the DB set up. Similarly, to not have to spend even more
time bludgeoning EF Core, these entities' properties will frequently be accessors
to value objects instead of automatic properties of value objects.

Future work could certainly stand to make this cleaner and not use EF Core.

Additionally, while I would like to not have members like `Character.Faction`
and the various ICollections, I also don't have the heart to fight EF Core even
more to get any version of that to work. Two possible ideas are using Fluent
API for the relation definitions, or extending basic entities to have those
members.

The internal members of these entities is done in order to allow EF Core to
still function while requiring assemblies outside of Data (and tests) to
utilize the service classes to get a faction from it's ID, and to get a list
of IDs related to something. This could be replaced by handling relations with
the Fluent API instead. This would also allow the entities to not have data
annotations as well.
