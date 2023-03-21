#nullable enable

using System;
using System.Collections;
using System.Data;

namespace Ainsworth.Results.Tests;

[TestClass]
public class ErrorTTErrorTests {

    private readonly int intError = -1;
    record struct ErrorStruct(int Code, string Message);
    private readonly ErrorStruct structError = new(6, "message");
    record class ErrorClass(int Code, string Message);
    private readonly ErrorClass classError = new(6, "message");

    [TestMethod]
    public void Constructor_Wraps_ExceptionArgument() {
        var errorResultI = new Error<string, int>(intError);
        Assert.AreEqual(intError, errorResultI.ErrorValue);
        var errorResultS = new Error<string, ErrorStruct>(structError);
        Assert.AreEqual(structError, errorResultS.ErrorValue);
        var errorResultC = new Error<string, ErrorClass>(classError);
        Assert.AreEqual(classError, errorResultC.ErrorValue);
        Assert.AreSame(classError, errorResultC.ErrorValue);
    }

    [TestMethod]
    public void CastOperator_Creates_ErrorResult() {
        var errorResultI = new Error<string, int>(intError);
        Assert.IsInstanceOfType(errorResultI, typeof(Error<string, int>));
        var errorResultS = (Error<string, ErrorStruct>)structError;
        Assert.IsInstanceOfType(errorResultS, typeof(Error<string, ErrorStruct>));
        var errorResultC = new Error<string, ErrorClass>(classError);
        Assert.IsInstanceOfType(errorResultC, typeof(Error<string, ErrorClass>));
    }

    [TestMethod]
    public void ErrorResult_Is_InstanceOfIResultT() {
        var errorResult = new Error<string, ErrorStruct>(structError);
        Assert.IsInstanceOfType(errorResult, typeof(IResult<string, ErrorStruct>));
    }

    [TestMethod]
    public void GetEnumerator_Creates_CorrectEnumerator() {
        var errorResult = new Error<string, ErrorStruct>(structError);
        var enumerator = errorResult.GetEnumerator();
        Assert.IsInstanceOfType(enumerator, typeof(ErrorEnumerator<string>));
    }

    [TestMethod]
    public void IEnumeratorGetEnumerator_Throws() {
        var errorResult = new Error<string, ErrorStruct>(structError);
        Assert.ThrowsException<NotImplementedException>(
            () => (errorResult as IEnumerable).GetEnumerator());
    }

    [TestMethod]
    public void Select_Retains_Errorn() {
        var errorResultI = new Error<string, int>(intError);
        var selectResultI = errorResultI.Select(v => int.Parse(v));
        Assert.AreEqual(intError, ((Error<int, int>)selectResultI).ErrorValue);
        var errorResultS = new Error<string, ErrorStruct>(structError);
        var selectResultS = errorResultS.Select(v => int.Parse(v));
        Assert.AreEqual(structError, ((Error<int, ErrorStruct>)selectResultS).ErrorValue);
        var errorResultC = new Error<string, ErrorClass>(classError);
        var selectResultC = errorResultC.Select(v => int.Parse(v));
        Assert.AreEqual(classError, ((Error<int, ErrorClass>)selectResultC).ErrorValue);
        Assert.AreSame(classError, ((Error<int, ErrorClass>)selectResultC).ErrorValue);
    }

    [TestMethod]
    public void Select_TransformsTo_NewType() {
        var errorResult = new Error<string, ErrorStruct>(structError);
        var selectResult= errorResult.Select(v => int.Parse(v));
        Assert.IsInstanceOfType(selectResult, typeof(IResult<int, ErrorStruct>));
    }

}
