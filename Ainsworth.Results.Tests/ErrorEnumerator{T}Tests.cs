#nullable enable

using System;
using System.Collections;

namespace Ainsworth.Results.Tests;

[TestClass]
public class ErrorEnumeratorTTests {

    private static readonly Exception wrappedException = new();

    [TestMethod]
    public void Constructor_Creates_EmptyEnumerator() {
        var enumerator = new ErrorEnumerator<string>("ignored value");
        Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void Current_Throws_AtEndOfCollection() {
        // ErrorEnumerator<T> implementation
        var enumerator = new ErrorEnumerator<string>("ignored value").Exhaust();
        var ex1 = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledOnExhaustedEnumerator, ex1.Message);
        // IEnumerator implementation
        var ienumerator = (IEnumerator)new ErrorEnumerator<string>("ignored value").Exhaust();
        var ex2 = Assert.ThrowsException<InvalidOperationException>(() => ienumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledOnExhaustedEnumerator, ex2.Message);
    }

    [TestMethod]
    public void Current_Throws_BeforeFirstMoveNextCall() {
        // ErrorEnumerator<T> implementation
        var enumerator = new ErrorEnumerator<string>("ignored value");
        var ex1 = Assert.ThrowsException<InvalidOperationException>(
            () => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledBeforeMoveNext, ex1.Message);
        // IEnumerator implementation
        var ienumerator = (IEnumerator)new ErrorEnumerator<string>("ignored value");
        var ex2 = Assert.ThrowsException<InvalidOperationException>(() => ienumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledBeforeMoveNext, ex2.Message);
    }

    [TestMethod]
    public void Reset_Resets_Enumerator() {

        // Create an enumerator, run it to the end, and reset it.
        var enumerator = new ErrorEnumerator<string>("ignored value");
        while (enumerator.MoveNext()) {
            // nothing to do
        }
        enumerator.Reset();

        // Check ErrorEnumerator_Throws_BeforeFirstMoveNextCall()
        var ex1 = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledBeforeMoveNext, ex1.Message);

        // Check Constructor_Creates_EmptyEnumerator
        Assert.IsFalse(enumerator.MoveNext());

        // Check ErrorEnumerator_Throws_AtEndOfCollection()
        var ex2 = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledOnExhaustedEnumerator, ex2.Message);
    }

}
