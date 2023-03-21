#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ainsworth.Results;

/// <summary>
///   An implementation of <see cref="IResult{T, TError}"/> indicating failure.
/// </summary>
/// <typeparam name="T">The type of the wrapped value.</typeparam>
/// <typeparam name="TError">The type of the wrapped error value.</typeparam>
public readonly struct Error<T, TError> : IResult<T, TError>
    where T: notnull
    where TError : notnull {

    /// <summary>
    /// Return the error value.
    /// </summary>
    /// <remarks>
    ///   This property is commonly called <i>return</i> in theoretical discussions
    ///   of monads and functional programming.
    /// </remarks>
    public TError ErrorValue { get; init; }

    /// <summary>
    ///   Initialize a new <see cref="Error{T}"></see> using a specified
    ///   <see cref="Exception"/>.
    /// </summary>
    /// <param name="errorValue">The error value.</param>
    internal Error(TError errorValue) {
        ErrorValue = errorValue;
    }

    /// <summary>
    ///   Cast operator to convert a <typeparamref name="TError"/> to an
    ///   <see cref="Error{T, TError}"/>.
    /// </summary>
    /// <param name="errorValue">The error value.</param>
    public static implicit operator Error<T, TError>(TError errorValue) => new(errorValue);

    /// <inheritdoc/>
    /// <remarks>
    ///   This method is commonly called <i>bind</i> or <i>map</i> in theoretical
    ///   discussions of monads and functional programming.
    /// </remarks>
    public IResult<TResult, TError> Select<TResult>(Func<T, TResult> _)
            where TResult : notnull
        => ErrorValue.ToResult<TResult, TError>();

    /// <summary>
    ///   Returns an enumerator that iterates through a collection comprising
    ///   the single <see cref="IResult{T, TError}"/> value.
    /// </summary>
    /// <returns>An empty enumerator (a failure has no value).</returns>
    public IEnumerator<T> GetEnumerator() => new ErrorEnumerator<T>();

    /// <summary>
    ///   Returns an enumerator that iterates through a collection comprising
    ///   the single <see cref="IResult{T, TError}"/> value.
    /// </summary>
    /// <returns>An empty enumerator (a failure has no value).</returns>
    [Obsolete("Obsolete: use strongly-typed version instead.")]
    //[ExcludeFromCodeCoverage] // trivally verified by inspection
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
}
