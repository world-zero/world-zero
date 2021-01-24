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
// DONE: add entity constructors w/ ones that take the corresponding DTO
//
// WIP: merge DTOs into entities
//      this is going to be BIG
//      be sure to draw this out
//      have IEntity note that entities shouldn't override clone, so they'll just
//          inherit a DTO clone from their concrete DTO parent
//      finish linking IFs to IFs then create the concrete entities' Clone (as DTO)
//      remove CloneAsEntity
//
// NOTE: I have adjusted Clone to return a dto so repos are going to complain aabout not getting an entity
//
// TODO: migrate repos - rename these to be entityDTO repos
//      this will likely cause Service to fail, but that's okay for now
//      first, be sure to copy GetUniqRules to the DTOs
//      wait: dtos will need to have configurable ids
//          ientity.id is get/set, but entity.id is get and throws exc on set
//          consider how to change entity.id.Set to allow me to change the value
//          if I give it just an auto set, then I would run into issues w/ changing ids
//              consider the flow of each repo operation and how this would effect it
//              when does insert and update clone again? what about search?
//                  insert doesnt set the id until it is saved, so no issues there
//                  GetById clones; make sure ALL other read ops clone too
//                  update could get weird
//                  save could be weird too
//          to not have to add IsIdSet to DTOs, could just set the ID after every save?
//              no IsIdSet is a terrible idea, add it to ientitydto somewhere probably
//                  remember to consider the various cases
//                  probably just migrate the isidset (along w/ getuniqrules) to dto
//          instead of isidset, clone then just check if id is null - how would this work w/ Name ids?
//          clone inputs every time; for insert, have another dict that holds the ref and sets the ID after saving
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