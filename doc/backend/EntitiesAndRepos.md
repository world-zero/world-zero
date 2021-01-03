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
chains and branches, adding and adjusting as they go. In the interest of time,
the entity repos don't have many entity-specific function members. These are
being built on an as-needed basis.  

While the repos should not have access to each other, this has the obvious flaw
of ruling out the viability of advanced SQL interactions. Conveniently, all of
the repos actually do know about each other's *data*, so you still have access
to writing advanced methods. For the DB entity repos, this would just be simple
SQL - for the RAM entity repos, this can be achieved via LINQ-control of
`IStaticRAMEntityRepo._data`.

## Spots where I am lazy

### RAM Entity Relation Repos

Some relational entities, like Comment and PraxisParticipant, have the usual
dual combination that are enforced to be unique, but they also have a third
member to track the count, making it a triple unique key. Because RAM repos are
more of a dev tool as opposed to a production database repo, these do not. As a
result, they will crash when they really shouldn't around this case.
