#nullable enable

using System;
using System.Collections;

namespace Ainsworth.Results.Tests;

[TestClass]
public class OkTTErrorTests {

    record struct ErrorStruct(int Code, string Message);

    private const string wrappedValue = "111";
    private const int intValue = 111;

    [TestMethod]
    public void Constructor_Wraps_ValueArgument() {
        var okResult = new Ok<string, ErrorStruct>(wrappedValue);
        Assert.AreSame(wrappedValue, okResult.Value);
    }

    [TestMethod]
    public void CastOperator_Creates_OkResult() {
        var okResult = (Ok<string, ErrorStruct>)wrappedValue;
        Assert.IsInstanceOfType(okResult, typeof(Ok<string, ErrorStruct>));
    }

    [TestMethod]
    public void OkResult_Is_InstanceOfIResultT() {
        var okResult = new Ok<string, ErrorStruct>(wrappedValue);
        ;
        Assert.IsInstanceOfType(okResult, typeof(IResult<string, ErrorStruct>));
    }

    [TestMethod]
    public void GetEnumerator_Creates_CorrectEnumerator() {
        var okResult = new Ok<string, ErrorStruct>(wrappedValue);
        var enumerator = okResult.GetEnumerator();
        Assert.IsInstanceOfType(enumerator, typeof(OkEnumerator<string>));
    }

    [TestMethod]
    public void IEnumerableGetEnumerator_Throws() {
        var okResult = new Ok<string, ErrorStruct>(wrappedValue);
        Assert.ThrowsException<NotImplementedException>(
            () => (okResult as IEnumerable).GetEnumerator());
    }

    [TestMethod]
    public void Select_Transforms_ToNewType() {
        var okResult = new Ok<string, ErrorStruct>(wrappedValue);
        var selectResult = okResult.Select(v => int.Parse(v));
        Assert.IsInstanceOfType(selectResult, typeof(IResult<int, ErrorStruct>));
    }

}
