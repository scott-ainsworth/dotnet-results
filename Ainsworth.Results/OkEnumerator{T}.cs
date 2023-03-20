#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ainsworth.Results;

/// <summary>
///   Supports simple iteration over the single value of an <see cref="IResult{T}"/>. 
/// </summary>
/// <typeparam name="T">The type of objects to enumerate.</typeparam>
internal struct OkEnumerator<T> : IEnumerator<T>
    where T : notnull {

    private readonly T value;
    private int index = -1;

    /// <summary>
    ///   Initialize a new <see cref="OkEnumerator{T}"/> using the specified value.
    /// </summary>
    /// <param name="value">The value (from an <see cref="Ok{T}"/>).</param>
    internal OkEnumerator(T value) {
        this.value = value;
    }

    /// <summary>
    ///   Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <value>The element in the collection at the current position of the enumerator.</value>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="Current"/> is
    ///   called before <see cref="MoveNext"/> is called; or, when the enumerator is
    ///   past the end of the list.</exception>
    public T Current => index switch {
        -1 => throw new InvalidOperationException(Messages.CurrentCalledBeforeMoveNext),
        0 => value,
        _ => throw new InvalidOperationException(Messages.CurrentCalledOnExhaustedEnumerator),
    };

    /// <summary>
    ///   Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <returns>The default value for <typeparamref name="T"/>; a failure has
    ///   not value.</returns>
    /// <exception cref="InvalidOperationException">Thrown when <see cref="Current"/> is
    ///   called before <see cref="MoveNext"/> is called.</exception>
    [Obsolete("Obsolete: use strongly-typed version instead.")]
    object IEnumerator.Current => Current;

    /// <summary>
    ///   Not applicable.
    /// </summary>
    [ExcludeFromCodeCoverage] // Does nothing
    public void Dispose() { /* not required */ }

    /// <summary>
    ///   Advance the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>
    ///   <see langword="true"/> if the enumerator sucessfully advanced to the first (and only)
    ///   element of the collection; <see langwork="false"/> if the enumerator has passed the
    ///   end of the colleciton.
    /// </returns>
    public bool MoveNext() => ++index == 0;

    /// <summary>
    ///   Sets the enumerator to its initial position, which is before the first element
    ///   in the collection.
    /// </summary>
    public void Reset() => index = -1;
}
