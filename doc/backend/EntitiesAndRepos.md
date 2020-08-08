# Entities and Repos

## Entities

Entities in this project serve as tuples of a table. As a result, they will not
contain references to other members, but rather foreign keys of references. The
`internal` constructor of these entities is to only be used by Dapper in
`Data/`, as it cannot convert built-in types to value objects, but it allows
for using a constructor of built-in types. As a result, these entities have a
constructor of built-in types that is used by Dapper to construct value objects
to then build the instances.  

Be aware that these Dapper-intended constructors will do absolutely no
validation, so they have unknown effects when used not by Dapper.

## Repos

As you would expect, the various repo interfaces mirror the entity interface
chains and branches, adding and adjusting as they go.
