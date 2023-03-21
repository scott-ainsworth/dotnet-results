#nullable enable

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Ainsworth.Results.Tests;

[TestClass]
public class ToResultTTests {

    record struct TestStruct(int i, string s);
    record class TestClass(int i, string s);

    private const int intValue = 111;
    private const string stringValue = "111";
    private readonly Exception ex = new Exception();

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForPrimitiveValueTypes() {

        var intResult = intValue.ToResult();
        var intErrorResult = ex.ToResult<int>();
        Assert.IsInstanceOfType(intResult, typeof(Ok<int>));
        Assert.IsInstanceOfType(intResult, typeof(IResult<int>));
        Assert.IsInstanceOfType(intErrorResult, typeof(Error<int>));
        Assert.IsInstanceOfType(intErrorResult, typeof(IResult<int>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForStructuredValueTypes() {

        var tupleValue = (intValue, stringValue);
        var tupleResult = tupleValue.ToResult();
        var tupleErrorResult = ex.ToResult<(int, string)>();
        Assert.IsInstanceOfType(tupleResult, typeof(Ok<(int, string)>));
        Assert.IsInstanceOfType(tupleResult, typeof(IResult<(int, string)>));
        Assert.IsInstanceOfType(tupleErrorResult, typeof(Error<(int, string)>));
        Assert.IsInstanceOfType(tupleErrorResult, typeof(IResult<(int, string)>));

        var structValue = new TestStruct(intValue, stringValue);
        var structResult = structValue.ToResult();
        var structErrorResult = ex.ToResult<TestStruct>();
        Assert.IsInstanceOfType(structResult, typeof(Ok<TestStruct>));
        Assert.IsInstanceOfType(structResult, typeof(IResult<TestStruct>));
        Assert.IsInstanceOfType(structErrorResult, typeof(Error<TestStruct>));
        Assert.IsInstanceOfType(structErrorResult, typeof(IResult<TestStruct>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForReferenceTypes() {

        var stringResult = stringValue.ToResult();
        var stringErrorResult = ex.ToResult<string>();
        Assert.IsInstanceOfType(stringResult, typeof(Ok<string>));
        Assert.IsInstanceOfType(stringResult, typeof(IResult<string>));
        Assert.IsInstanceOfType(stringErrorResult, typeof(Error<string>));
        Assert.IsInstanceOfType(stringErrorResult, typeof(IResult<string>));

        var listResult = new ArrayList().ToResult();
        var listErrorResult = ex.ToResult<ArrayList>();
        Assert.IsInstanceOfType(listResult, typeof(Ok<ArrayList>));
        Assert.IsInstanceOfType(listResult, typeof(IResult<ArrayList>));
        Assert.IsInstanceOfType(listErrorResult, typeof(Error<ArrayList>));
        Assert.IsInstanceOfType(listErrorResult, typeof(IResult<ArrayList>));

        var classValue = new TestClass(intValue, stringValue);
        var classResult = classValue.ToResult();
        var classErrorResult = ex.ToResult<TestClass>();
        Assert.IsInstanceOfType(classResult, typeof(Ok<TestClass>));
        Assert.IsInstanceOfType(classResult, typeof(IResult<TestClass>));
        Assert.IsInstanceOfType(classErrorResult, typeof(Error<TestClass>));
        Assert.IsInstanceOfType(classErrorResult, typeof(IResult<TestClass>));

    }
}
