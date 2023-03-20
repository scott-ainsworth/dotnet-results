#nullable enable

using System;
using System.Collections;

namespace Ainsworth.Results.Tests;

[TestClass]
public class OkEnumeratorTTests {

    private static readonly int wrappedValue = 111;

    [TestMethod]
    public void Constructor_Creates_SingleValueEnumerator() {
        var enumerator = new OkEnumerator<int>(wrappedValue);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.IsFalse(enumerator.MoveNext());
    }

    [TestMethod]
    public void Constructor_Wraps_CorrectValue() {
        // OkEnumerator<T> implementation
        var enumerator = new OkEnumerator<int>(wrappedValue);
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(wrappedValue, enumerator.Current);
        // IEnumerator implementation
        var ienumerator = (IEnumerator)new OkEnumerator<int>(wrappedValue);
        Assert.IsTrue(ienumerator.MoveNext());
        Assert.AreEqual(wrappedValue, ienumerator.Current);
    }

    [TestMethod]
    public void Current_Throws_AtEndOfCollection() {
        // OkEnumerator<T> implementation
        var enumerator = new OkEnumerator<int>(wrappedValue).Exhaust();
        var ex = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledOnExhaustedEnumerator, ex.Message);
        // IEnumerator implementation
        var ienumerator = (IEnumerator)new OkEnumerator<int>(wrappedValue).Exhaust();
        var ex2 = Assert.ThrowsException<InvalidOperationException>(() => ienumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledOnExhaustedEnumerator, ex2.Message);
    }

    [TestMethod]
    public void Current_Throws_BeforeFirstMoveNextCall() {
        // OkEnumerator<T> implementation
        var enumerator = new OkEnumerator<int>(wrappedValue);
        var ex = Assert.ThrowsException<InvalidOperationException>(
            () => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledBeforeMoveNext, ex.Message);
        // IEnumerator implementation
        var ienumerator = (IEnumerator)new OkEnumerator<int>(wrappedValue);
        var ex2 = Assert.ThrowsException<InvalidOperationException>(() => ienumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledBeforeMoveNext, ex2.Message);
    }

    [TestMethod]
    public void Reset_Resets_Enumerator() {

        // Create an enumerator, run it to the end, and reset it.
        var enumerator = new OkEnumerator<int>(wrappedValue);
        _ = enumerator.Exhaust();
        enumerator.Reset();

        // Check ErrorEnumerator_Throws_BeforeFirstMoveNextCall()
        var ex1 = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledBeforeMoveNext, ex1.Message);

        // Check Constructor_Creates_SingleValueEnumerator
        //   and Constructor_Wraps_CorrectValue
        Assert.IsTrue(enumerator.MoveNext());
        Assert.AreEqual(wrappedValue, enumerator.Current);
        Assert.IsFalse(enumerator.MoveNext());

        // Check ErrorEnumerator_Throws_AtEndOfCollection()
        var ex2 = Assert.ThrowsException<InvalidOperationException>(() => enumerator.Current);
        Assert.AreEqual(Messages.CurrentCalledOnExhaustedEnumerator, ex2.Message);
    }

}
