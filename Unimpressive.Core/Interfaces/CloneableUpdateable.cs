using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unimpressive.Core.Interfaces
{
    /// <inheritdoc />
    /// <summary>
    /// Simple strongly typed wrapper for ICloneable
    /// </summary>
    /// <typeparam name="T">Any type</typeparam>
    public interface ICloneable<out T> : ICloneable
    {
        /// <summary>
        /// Clone the object with strong typing
        /// </summary>
        /// <returns>The clone of the original object</returns>
        T Copy();
    }

    /// <summary>
    /// Interface for updateable but cloneable objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IUpdateable<in T>
    {
        /// <summary>
        /// Update data of this object from an other object
        /// </summary>
        /// <param name="other">The other instance to update from</param>
        void UpdateFrom(T other);
    }
}
