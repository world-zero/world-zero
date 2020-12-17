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

### Constants

There are various constant entities that the system needs in order to operate.
For more, see `IConstantEntities`.
