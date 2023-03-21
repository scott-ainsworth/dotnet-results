#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace Ainsworth.Results;

/// <summary>
///   An implementation of <see cref="IResult{T}"/> indicating success.
/// </summary>
/// <typeparam name="T">The type of the value wrapped by <see cref="IResult{T}"/>.</typeparam>
/// <typeparam name="TError">The type of the error value wrapped by
///    <see cref="IResult{T}"/>.</typeparam>
public readonly struct Ok<T, TError> : IResult<T, TError>
    where T : notnull
    where TError: notnull {

    /// <summary>
    ///   Return the success value.
    /// </summary>
    /// <remarks>
    ///   This property is commonly called <i>return</i> or <i>unit</i> in theoretical
    ///   discussions of monads and functional programming.
    /// </remarks>
    public T Value { get; init; }

    /// <summary>
    /// Initialize a new <see cref="Ok{T, TError}"/>
    /// </summary>
    /// <param name="value"></param>
    internal Ok(T value) {
        Value = value;
    }

    /// <summary>
    /// Cast operator to convert a <typeparamref name="T"/> to an <see cref="Ok{T, TError}"/>.
    /// </summary>
    /// <param name="value">The value to cast.</param>
    public static implicit operator Ok<T, TError>(T value) => new(value);

    /// <summary>
    ///   Map the <see cref="IResult{T, TError}"/> value using a specified selector function.
    /// </summary>
    /// <typeparam name="TResult">The type after applying <paramref name="selector"/>
    ///   to the value.</typeparam>
    /// <param name="selector">A transform function to apply to the value.</param>
    /// <returns>A new <see cref="IResult{T, TError}"/> containing the results of applying
    ///   <paramref name="selector"/>.</returns>
    /// <remarks>
    ///   This method is commonly called <i>bind</i> or <i>map</i> in theoretical
    ///   discussions of monads and functional programming.
    /// </remarks>
    public IResult<TResult, TError>
        Select<TResult>(Func<T, TResult> selector)
            where TResult: notnull
        => selector(Value).ToResult<TResult, TError>();

    /// <summary>
    ///   Returns an enumerator that iterates through a collection comprising
    ///   the single <see cref="IResult{T, TError}"/> value.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through a collection
    ///   consisting of <see cref="Value"/>.</returns>
    public IEnumerator<T> GetEnumerator() => new OkEnumerator<T>(Value);

    /// <summary>
    ///   Returns an enumerator that iterates through a collection comprising
    ///   the single <see cref="IResult{T, TError}"/> value.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through a collection
    ///   consisting of <see cref="Value"/>.</returns>
    [Obsolete("Obsolete: use strongly-typed version instead.")]
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

}
