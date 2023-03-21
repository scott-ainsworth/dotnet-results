#nullable enable

using System;

namespace Ainsworth.Results;

/// <summary>
/// Conversions to <see cref="IResult{T}"/>
/// </summary>
public static class Result {

    // ---------------------------------------------------------
    // Conversions from value type T and Exception to IResult<T>
    // ---------------------------------------------------------

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="value">The value to wrap</param>
    /// <returns>A successful <see cref="IResult{T}"/> wrapping
    ///   <paramref name="value"/>.</returns>
    /// <seealso cref="ToResult{T}(T)"/>
    public static IResult<T> From<T>(T value) where T: notnull
        => (Ok<T>)value!;

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="ex">The <see cref="Exception"/> to wrap</param>
    /// <returns>A failure <see cref="IResult{T}"/> with no value wrapping
    ///   <paramref name="ex"/>.</returns>
    /// <seealso cref="ToResult{T}(Exception)"/>
    public static IResult<T> From<T>(Exception ex) where T : notnull
        => (Error<T>)ex;

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="value">The value to wrap</param>
    /// <returns>A successful <see cref="IResult{T}"/> wrapping
    ///   <paramref name="value"/>.</returns>
    /// <seealso cref="From{T}(T)"/>
    /// <remarks>
    ///   This is simply the functional form of <see cref="From{T}(T)"/>.
    /// </remarks>
    public static IResult<T> ToResult<T>(this T value) where T : notnull
        => (Ok<T>)value!;

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="ex">The <see cref="Exception"/> to wrap</param>
    /// <returns>A failure <see cref="IResult{T}"/> with no value wrapping
    ///   <paramref name="ex"/>.</returns>
    /// <seealso cref="From{T}(Exception)"/>
    /// <remarks>
    ///   This is simply the functional form of <see cref="From{T}(Exception)"/>.
    /// </remarks>
    public static IResult<T> ToResult<T>(this Exception ex) where T : notnull
        => (Error<T>)ex;

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from the results of executing
    ///   a specified function.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T}"/>.</typeparam>
    /// <param name="func">The function to execute.  This function will either return
    ///   a value of type <typeparamref name="T"/> or throw an exception.</param>
    /// <returns></returns>
    /// <remarks>
    ///   <paramref name="func"/> will either return a value of type <typeparamref name="T"/>
    ///   or throw an exception.  When a value is returned, this method returns an
    ///   <see cref="Ok{T}"/>.  When an exeption is thrown, this method returns an
    ///   <see cref="Error{T}"/>.
    /// </remarks>
    public static IResult<T> From<T>(Func<T> func) where T : notnull {
        try {
            return func().ToResult();
        } catch (Exception ex) {
            return ex.ToResult<T>();
        }
    }

    // -----------------------------------------------------------------
    // Conversions from value type T and Exception to IResult<T, TError>
    // -----------------------------------------------------------------

    /// <summary>
    ///   Create a new <see cref="IResult{T, TError}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T, TError}"/>.</typeparam>
    /// <typeparam name="TError">The type of the error value wrapped by the resulting
    ///    <see cref="IResult{T, TError}"/>.</typeparam>
    /// <param name="value">The value to wrap</param>
    /// <returns>A successful <see cref="IResult{T, TError}"/> wrapping
    ///   <paramref name="value"/>.</returns>
    /// <seealso cref="ToResult{T, TError}(T)"/>
    public static IResult<T, TError> From<T, TError>(T value)
            where T : notnull
            where TError : notnull
        => (Ok<T, TError>)value!;

    /// <summary>
    ///   Create a new <see cref="IResult{T}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T, TError}"/>.</typeparam>
    /// <typeparam name="TError">The type of the error value wrapped by the resulting
    ///    <see cref="IResult{T, TError}"/>.</typeparam>
    /// <param name="errorValue">The <typeparamref name="TError"/> to wrap</param>
    /// <returns>A failure <see cref="IResult{T, TError}"/> with no value wrapping
    ///   a <typeparamref name="TError"/>.</returns>
    /// <seealso cref="ToResult{T, TError}(TError)"/>
    public static IResult<T, TError> From<T, TError>(TError errorValue)
            where T : notnull
            where TError : notnull
        => (Error<T, TError>)errorValue;

    /// <summary>
    ///   Create a new <see cref="IResult{T, TError}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T, TError}"/>.</typeparam>
    /// <typeparam name="TError">The type of the error value wrapped by the resulting
    ///    <see cref="IResult{T, TError}"/>.</typeparam>
    /// <param name="value">The value to wrap</param>
    /// <returns>A successful <see cref="IResult{T, TError}"/> wrapping
    ///   <paramref name="value"/>.</returns>
    /// <seealso cref="From{T, TError}(T)"/>
    /// <remarks>
    ///   This is simply the functional form of <see cref="From{T, TError}(T)"/>.
    /// </remarks>
    public static IResult<T, TError> ToResult<T, TError>(this T value)
            where T : notnull
            where TError : notnull
        => (Ok<T, TError>)value!;

    /// <summary>
    ///   Create a new <see cref="IResult{T, TError}"/> from a specifed value.
    /// </summary>
    /// <typeparam name="T">The type of the value wrapped by the returned
    ///   <see cref="IResult{T, TError}"/>.</typeparam>
    /// <typeparam name="TError">The type of the error value wrapped by the resulting
    ///    <see cref="IResult{T, TError}"/>.</typeparam>
    /// <param name="errorValue">The <typeparamref name="TError"/> to wrap</param>
    /// <returns>A failure <see cref="IResult{T, TError}"/> with no value wrapping
    ///   <paramref name="errorValue"/>.</returns>
    /// <seealso cref="From{T, TError}(TError)"/>
    /// <remarks>
    ///   This is simply the functional form of <see cref="From{T, TError}(TError)"/>.
    /// </remarks>
    public static IResult<T, TError> ToResult<T, TError>(this TError errorValue)
            where T : notnull
            where TError : notnull
        => (Error<T, TError>)errorValue;

}
