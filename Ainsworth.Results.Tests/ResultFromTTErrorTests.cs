#nullable enable

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Ainsworth.Results.Tests;

[TestClass]
public class ResultFromTTErrorTests {

    record struct TestStruct(int i, string s);
    record class TestClass(int i, string s);
    record struct ErrorStruct(int Code, string Message);

    private const int intValue = 111;
    private const string stringValue = "111";
    private readonly ErrorStruct structError = new(6, "message");

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForPrimitiveValueTypes() {

        var intResult = Result.From<int, ErrorStruct>(intValue);
        var intErrorResult = Result.From<int, ErrorStruct>(structError);
        Assert.IsInstanceOfType(intResult, typeof(Ok<int, ErrorStruct>));
        Assert.IsInstanceOfType(intResult, typeof(IResult<int, ErrorStruct>));
        Assert.IsInstanceOfType(intErrorResult, typeof(Error<int, ErrorStruct>));
        Assert.IsInstanceOfType(intErrorResult, typeof(IResult<int, ErrorStruct>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForStructuredValueTypes() {

        var tupleValue = (intValue, stringValue);
        var tupleResult = Result.From<(int, string), ErrorStruct>(tupleValue);
        var tupleErrorResult = Result.From<(int, string), ErrorStruct>(structError);
        Assert.IsInstanceOfType(tupleResult, typeof(Ok<(int, string), ErrorStruct>));
        Assert.IsInstanceOfType(tupleResult, typeof(IResult<(int, string), ErrorStruct>));
        Assert.IsInstanceOfType(tupleErrorResult, typeof(Error<(int, string), ErrorStruct>));
        Assert.IsInstanceOfType(tupleErrorResult, typeof(IResult<(int, string), ErrorStruct>));

        var structValue = new TestStruct(intValue, stringValue);
        var structResult = Result.From<TestStruct, ErrorStruct>(structValue);
        var structErrorResult = Result.From<TestStruct, ErrorStruct>(structError);
        Assert.IsInstanceOfType(structResult, typeof(Ok<TestStruct, ErrorStruct>));
        Assert.IsInstanceOfType(structResult, typeof(IResult<TestStruct, ErrorStruct>));
        Assert.IsInstanceOfType(structErrorResult, typeof(Error<TestStruct, ErrorStruct>));
        Assert.IsInstanceOfType(structErrorResult, typeof(IResult<TestStruct, ErrorStruct>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForReferenceTypes() {

        var stringResult = Result.From<string, ErrorStruct>(stringValue);
        var stringErrorResult = Result.From<string, ErrorStruct>(structError);
        Assert.IsInstanceOfType(stringResult, typeof(Ok<string, ErrorStruct>));
        Assert.IsInstanceOfType(stringResult, typeof(IResult<string, ErrorStruct>));
        Assert.IsInstanceOfType(stringErrorResult, typeof(Error<string, ErrorStruct>));
        Assert.IsInstanceOfType(stringErrorResult, typeof(IResult<string, ErrorStruct>));

        var listResult = Result.From<ArrayList, ErrorStruct>(new ArrayList());
        var listErrorResult = Result.From<ArrayList, ErrorStruct>(structError);
        Assert.IsInstanceOfType(listResult, typeof(Ok<ArrayList, ErrorStruct>));
        Assert.IsInstanceOfType(listResult, typeof(IResult<ArrayList, ErrorStruct>));
        Assert.IsInstanceOfType(listErrorResult, typeof(Error<ArrayList, ErrorStruct>));
        Assert.IsInstanceOfType(listErrorResult, typeof(IResult<ArrayList, ErrorStruct>));

        var classValue = new TestClass(intValue, stringValue);
        var classResult = Result.From<TestClass, ErrorStruct>(classValue);
        var classErrorResult = Result.From<TestClass, ErrorStruct>(structError);
        Assert.IsInstanceOfType(classResult, typeof(Ok<TestClass, ErrorStruct>));
        Assert.IsInstanceOfType(classResult, typeof(IResult<TestClass, ErrorStruct>));
        Assert.IsInstanceOfType(classErrorResult, typeof(Error<TestClass, ErrorStruct>));
        Assert.IsInstanceOfType(classErrorResult, typeof(IResult<TestClass, ErrorStruct>));

    }

}

