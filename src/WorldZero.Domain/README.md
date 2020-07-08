# Domain Notes

## Why are the ORM models and their logic two different classes?

While you could certainly merge these models into their corresponding entity,
I like the separation of ORM classes and their business entity as it is better
separates the concerns of these two very different objects. As a result, these
models are really publicly visible (at least at the time of writing) mementos,
where their corresponding entity is a snapshot class, and the repo that manages
the collection of an entity is a caretaker.
