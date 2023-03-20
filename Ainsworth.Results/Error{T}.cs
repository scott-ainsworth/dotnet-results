#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace Ainsworth.Results;

/// <summary>
///   An implementation of <see cref="IResult{T}"/> indicating failure.
/// </summary>
/// <typeparam name="T">The type of the value wrapped by <see cref="IResult{T}"/>.</typeparam>
public readonly struct Error<T> : IResult<T>
    where T: notnull {

    /// <summary>
    /// Return the error value.
    /// </summary>
    /// <remarks>
    ///   This property is commonly called <i>return</i> in theoretical discussions
    ///   of monads and functional programming.
    /// </remarks>
    public Exception Exception { get; init; }

    /// <summary>
    /// Initialize a new <see cref="Error{T}"></see> using a specified <see cref="Exception"/>.
    /// </summary>
    /// <param name="ex">The <see cref="Exception"/>.</param>
    internal Error(Exception ex) {
        Exception = ex;
    }

    /// <summary>
    /// Cast operator to convert an <see cref="Exception"/> to an <see cref="Error{T}"/>.
    /// </summary>
    /// <param name="ex">The <see cref="Exception"/> to cast.</param>
    public static implicit operator Error<T>(Exception ex) => new(ex);

    /// <inheritdoc/>
    /// <remarks>
    ///   This method is commonly called <i>bind</i> or <i>map</i> in theoretical
    ///   discussions of monads and functional programming.
    /// </remarks>
    public IResult<TResult> Select<TResult>(Func<T, TResult> _)
            where TResult : notnull
        => Exception.ToResult<TResult>();

    /// <summary>
    ///   Returns an enumerator that iterates through a collection comprising
    ///   the single <see cref="IResult{T}"/> value.
    /// </summary>
    /// <returns>An empty enumerator (a failure has no value).</returns>
    public IEnumerator<T> GetEnumerator() => new ErrorEnumerator<T>();

    /// <summary>
    ///   Returns an enumerator that iterates through a collection comprising
    ///   the single <see cref="IResult{T}"/> value.
    /// </summary>
    /// <returns>An empty enumerator (a failure has no value).</returns>
    [Obsolete("Obsolete: use strongly-typed version instead.")]
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
