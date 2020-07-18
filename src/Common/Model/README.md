# Models

Currently, these exist to be data models that use EF Core's data annotations to
handle almost all of the DB set up. Similarly, to not have to spend even more
time bludgeoning EF Core, these models' properties will frequently be accessors
to value objects instead of automatic properties of value objects.

Future work could certainly stand to make this cleaner and not use EF Core.
