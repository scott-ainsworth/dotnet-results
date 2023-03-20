#nullable enable

using System;

namespace Ainsworth.Results.Tests;

[TestClass]
public class OkTTests {

    private const string wrappedValue = "111";
    private const int intValue = 111;

    [TestMethod]
    public void Constructor_Wraps_ValueArgument() {
        var okResult = new Ok<string>(wrappedValue);
        Assert.AreSame(wrappedValue, okResult.Value);
    }

    [TestMethod]
    public void CastOperator_Creates_OkResult() {
        var okResult = (Ok<string>)wrappedValue;
        Assert.IsInstanceOfType(okResult, typeof(Ok<string>));
    }

    [TestMethod]
    public void OkResult_Is_InstanceOfIResultT() {
        var okResult = new Ok<string>(wrappedValue);;
        Assert.IsInstanceOfType(okResult, typeof(IResult<string>));
    }

    [TestMethod]
    public void GetEnumerator_Creates_CorrectEnumerator() {
        var okResult = new Ok<string>(wrappedValue);
        var enumerator = okResult.GetEnumerator();
        Assert.IsInstanceOfType(enumerator, typeof(OkEnumerator<string>));
    }

    [TestMethod]
    public void Select_Transforms_ToNewType() {
        var okResult = new Ok<string>(wrappedValue);
        var selectResult = okResult.Select(v => int.Parse(v));
        Assert.IsInstanceOfType(selectResult, typeof(IResult<int>));
    }

}
