using System;

/* Goal
    IDTO <- IEntityDTO <- IEntity <- IChar <- UnsafeChar
                        <- ICharDTO <- CharDTO

        where ICharDTO has immutable properties
        where CharDTO has a dapper constructor (added to the DB repo implementation ticket)
            repos should use entity DTOs instead of entities directly
            reg service classes should take these whereas deletion/update should take entities
                search services return proper entities
                    what about if they got two versions of an entity and
                            only updated one of them? or something like that
        where IChar can have methods but not recommended
        where UnsafeChar has an ICharDTO constructor and clones it into memory which the inherited properties get from
            Be cautious of ICharDTO implementations that have a shallow Clone
        where IEntiy.Clone() will return a clone *of the DTO* (return an IEntityDTO)
            drop CloneAsEntity too
*/

// DONE: create unspecified DTOs
// DONE: create the specified DTO IFs
// DONE: create unspecified primary entity DTOs
// DONE: create unspecified relation entity DTOs
// DONE: create specified primary entity DTOs
// DONE: create specified relation entity DTOs
// WIP: add entity constructors w/ ones that take the corresponding DTO
//      first, update code; then update the tests in one swoop
//
// TODO: merge DTOs into entities
//      this is going to be BIG
//      be sure to draw this out
//      have IEntity note that entities shouldn't override clone, so they'll just
//          inherit a DTO clone from their concrete DTO parent
//
// TODO: migrate repos
//      this will likely cause Service to fail, but that's okay for now
//      first, be sure to copy GetUniqRules to the DTOs
//      when mirgrated, remove the GetUniqRules from entities
//
// TODO: migrage services to use DTOs where appropriate
//      I{Enttiy}Reg should def use DTOs

/* TODO: migrate entity implementations into their service IF
    Create w0.test.tools which exposes these to the testing projects?

    be sure to put the ABCs w/in IEntityService or whereever to make sure people arent casting
        I may want to have that partial (?) class keyword where the class can be defined in several
            locations so I can still keep them in independent files
*/

// TODO: make UMLs for entitise, DTOs, and possibly services


namespace WorldZero.Common.Interface.DTO
{
    /// <summary>
    /// This is the abstraction for Data Transfer Objects (DTOs) for World
    /// Zero.
    /// </summary>
    /// <remarks>
    /// DTOs can be immutable or mutable as desired.
    /// </remarks>
    public interface IDTO
        : ICloneable,
        IEquatable<IDTO>
    {
        int GetHashCode();
    }
}