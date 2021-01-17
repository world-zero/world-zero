using System;

// DONE: create unspecified primary entity DTOs
// DONE: create unspecified relation entity DTOs
// DONE: create specified primary entity DTOs
// TODO: create specified relation entity DTOs
//
// TODO: merge DTOs into entities
//      this is going to be BIG
//      be sure to draw this out
//      be sure to copy GetUniqRules to the DTOs (will remove from entities during repo type migration)
//      have IEntity note that entities shouldn't override clone, so they'll just
//          inherit a DTO clone from their concrete DTO parent
//
// TODO: migrate repos; this will cause Service to fail, but that's okay for now
//      when mirgrated, remove the GetUniqRules stuffs from entities

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