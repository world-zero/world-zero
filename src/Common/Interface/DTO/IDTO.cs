using System;

// DONE: create unspecified primary entity DTOs
// DONE: create unspecified relation entity DTOs
// TODO: create specified primary entity DTOs
// TODO: create specified relation entity DTOs

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
        IEquatable<IDTO>,
        IEquatable<object>
    {
        int GetHashCode();
    }
}