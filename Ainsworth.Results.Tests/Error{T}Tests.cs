#nullable enable

using System;
using System.Collections;

namespace Ainsworth.Results.Tests;

[TestClass]
public class ErrorTTests {

    private static readonly Exception wrappedException = new();

    [TestMethod]
    public void Constructor_Wraps_ExceptionArgument() {
        var errorResult = new Error<string>(wrappedException);
        Assert.AreSame(wrappedException, errorResult.Exception);
    }

    [TestMethod]
    public void CastOperator_Creates_ErrorResult() {
        var errorResult = (Error<string>)wrappedException;
        Assert.IsInstanceOfType(errorResult, typeof(Error<string>));
    }

    [TestMethod]
    public void ErrorResult_Is_InstanceOfIResultT() {
        var errorResult = new Error<string>(wrappedException);
        Assert.IsInstanceOfType(errorResult, typeof(IResult<string>));
    }

    [TestMethod]
    public void GetEnumerator_Creates_CorrectEnumerator() {
        var errorResult = new Error<string>(wrappedException);
        var enumerator = errorResult.GetEnumerator();
        Assert.IsInstanceOfType(enumerator, typeof(ErrorEnumerator<string>));
    }

    [TestMethod]
    public void IEnumeratorGetEnumerator_Throws() {
        var errorResult = new Error<string>(wrappedException);
        Assert.ThrowsException<NotImplementedException>(
            () => (errorResult as IEnumerable).GetEnumerator());
    }

    [TestMethod]
    public void IsOk_Returns_False() {
        var errorResult = new Error<string>(wrappedException);
        Assert.IsFalse(errorResult.IsOk);
    }

    [TestMethod]
    public void IsError_Returns_True() {
        var errorResult = new Error<string>(wrappedException);
        Assert.IsTrue(errorResult.IsError);
    }

    [TestMethod]
    public void Select_Retains_Exception() {
        var errorResult = new Error<string>(wrappedException);
        var selectResult= errorResult.Select(v => int.Parse(v));
        Assert.AreSame(wrappedException, ((Error<int>)selectResult).Exception);
    }

    [TestMethod]
    public void Select_TransformsTo_NewType() {
        var errorResult = new Error<string>(wrappedException);
        var selectResult= errorResult.Select(v => int.Parse(v));
        Assert.IsInstanceOfType(selectResult, typeof(IResult<int>));
    }

}
