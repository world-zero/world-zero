using System;

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