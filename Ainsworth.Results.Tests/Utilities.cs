#nullable enable

using System;
using System.Collections;
using System.Globalization;

namespace Ainsworth.Results.Tests;

/// <summary>
///   Various functions and extension methods to simplify tests.
/// </summary>
public static class Utilities {

    /// <summary>
    ///   Exhaust an enumerator.
    /// </summary>
    /// <typeparam name="TEnumerator">The type of the enumerator, which must implement
    ///   <see cref="IEnumerator{T}"/>.</typeparam>
    /// <param name="enumerator">The to exhaust.</param>
    /// <returns><paramref name="enumerator"/>.</returns>
    public static TEnumerator Exhaust<TEnumerator>(this TEnumerator enumerator)
        where TEnumerator : IEnumerator {

        while (enumerator.MoveNext()) { /* Nothing to do */ }
        return enumerator;
    }
}

