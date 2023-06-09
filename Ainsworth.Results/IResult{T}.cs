#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;

namespace Ainsworth.Results;

/// <summary>
///   A monad for sane error handling.
/// </summary>
/// <typeparam name="T">The type of the value wrapped by <see cref="IResult{T}"/>.</typeparam>
/// <remarks>
///   This interface and its implementations are modeled after F#'s Result type.
/// </remarks>
/// <seealso href="https://learn.microsoft.com/en-us/dotnet/fsharp/language-reference/results"/>
public interface IResult<T> : IEnumerable<T>
    where T : notnull {

    /// <summary>
    ///   Determine if an <see cref="IResult{T, TError}"/> is a result.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this <see cref="IResult{T}"/> is a result;
    ///   <see langword="false"/> if it is an error.
    /// </value>
    bool IsOk { get; }

    /// <summary>
    ///   Determine if an <see cref="IResult{T, TError}"/> is an error.
    /// </summary>
    /// <value>
    ///   <see langword="true"/> if this <see cref="IResult{T}"/> is an error;
    ///   <see langword="false"/> if it is a result.
    /// </value>
    bool IsError { get; }

    /// <summary>
    ///   Map an <see cref="IResult{T}"/> value using a specified selector function.
    /// </summary>
    /// <typeparam name="TResult">The type after applying <paramref name="selector"/>
    ///   to the value.</typeparam>
    /// <param name="selector">A transform function to apply to the value.</param>
    /// <returns>A new <see cref="IResult{T}"/> containing the results of applying
    ///   <paramref name="selector"/>.</returns>
    /// <remarks>
    ///   This method is commonly called <i>bind</i> or <i>map</i> in other programming
    ///   languages and theoretical discussions about monads and functional programming.
    /// </remarks>
    IResult<TResult> Select<TResult>(Func<T, TResult> selector)
           where TResult : notnull;

}
