# Entity Service Classes

As described in the documentation's primary README, entity service classes are
classes that use repositories to enforce the various application logic that we
desire, broken up by the CRUD operation. This section contains notes on these
classes.

## General

When furthering CUD development on Vote, Praxis, and PraxisParticipant (PP)
service classes, you really want to read all of the CUD Praxis / PP docs. This
is because these are extremely intertwined.  

Be aware that these two rules in particular are relied upon heavily:

- A Praxis must have at least one PP.
- A Player can only have a single Character per Praxis.

Additionally, be mindful of Dueling (`Praxis.AreDueling`).  

We do not want service classes to boot people out of things as a side effect of
an operation. Consider the case where a task with `MinLevel` of 3 has several
praxises from people who's level is also 3. If we changed the task's `MinLevel`
to 4, then we would not want to revoke those praxises. The rule of thumb is
that we do not want to cross-/re-validate after registration has completed,
excluding the obvious cases where this rule is not absolute. In summary, we do
not want to cascade an update, but we do want to cascade deletions.

### Constants

There are various constant entities that the system needs in order to operate.
For more, see `IConstantEntities`.
