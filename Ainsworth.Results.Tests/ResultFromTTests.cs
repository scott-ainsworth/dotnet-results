#nullable enable

using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Ainsworth.Results.Tests;

[TestClass]
public class ResultFromTTests {

    record struct TestStruct(int i, string s);
    record class TestClass(int i, string s);

    private const int intValue = 111;
    private const string stringValue = "111";
    private readonly Exception ex = new Exception();

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForPrimitiveValueTypes() {

        var intResult = Result.From(intValue);
        var intErrorResult = Result.From<int>(ex);
        Assert.IsInstanceOfType(intResult, typeof(Ok<int>));
        Assert.IsInstanceOfType(intResult, typeof(IResult<int>));
        Assert.IsInstanceOfType(intErrorResult, typeof(Error<int>));
        Assert.IsInstanceOfType(intErrorResult, typeof(IResult<int>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForStructuredValueTypes() {

        var tupleValue = (intValue, stringValue);
        var tupleResult = Result.From(tupleValue);
        var tupleErrorResult = Result.From<(int, string)>(ex);
        Assert.IsInstanceOfType(tupleResult, typeof(Ok<(int, string)>));
        Assert.IsInstanceOfType(tupleResult, typeof(IResult<(int, string)>));
        Assert.IsInstanceOfType(tupleErrorResult, typeof(Error<(int, string)>));
        Assert.IsInstanceOfType(tupleErrorResult, typeof(IResult<(int, string)>));

        var structValue = new TestStruct(intValue, stringValue);
        var structResult = Result.From(structValue);
        var structErrorResult = Result.From<TestStruct>(ex);
        Assert.IsInstanceOfType(structResult, typeof(Ok<TestStruct>));
        Assert.IsInstanceOfType(structResult, typeof(IResult<TestStruct>));
        Assert.IsInstanceOfType(structErrorResult, typeof(Error<TestStruct>));
        Assert.IsInstanceOfType(structErrorResult, typeof(IResult<TestStruct>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_ForReferenceTypes() {

        var stringResult = Result.From(stringValue);
        var stringErrorResult = Result.From<string>(ex);
        Assert.IsInstanceOfType(stringResult, typeof(Ok<string>));
        Assert.IsInstanceOfType(stringResult, typeof(IResult<string>));
        Assert.IsInstanceOfType(stringErrorResult, typeof(Error<string>));
        Assert.IsInstanceOfType(stringErrorResult, typeof(IResult<string>));

        var listResult = Result.From(new ArrayList());
        var listErrorResult = Result.From<ArrayList>(ex);
        Assert.IsInstanceOfType(listResult, typeof(Ok<ArrayList>));
        Assert.IsInstanceOfType(listResult, typeof(IResult<ArrayList>));
        Assert.IsInstanceOfType(listErrorResult, typeof(Error<ArrayList>));
        Assert.IsInstanceOfType(listErrorResult, typeof(IResult<ArrayList>));

        var classValue = new TestClass(intValue, stringValue);
        var classResult = Result.From(classValue);
        var classErrorResult = Result.From<TestClass>(ex);
        Assert.IsInstanceOfType(classResult, typeof(Ok<TestClass>));
        Assert.IsInstanceOfType(classResult, typeof(IResult<TestClass>));
        Assert.IsInstanceOfType(classErrorResult, typeof(Error<TestClass>));
        Assert.IsInstanceOfType(classErrorResult, typeof(IResult<TestClass>));

    }

    [TestMethod]
    public void ResultFrom_CreatesCorrectType_Lambdas() {

        var lambdaResult = Result.From(() => "X");
        var lambdaErrorResult = Result.From<string>(() => throw ex);
        Assert.IsInstanceOfType(lambdaResult, typeof(Ok<string>));
        Assert.IsInstanceOfType(lambdaResult, typeof(IResult<string>));
        Assert.IsInstanceOfType(lambdaErrorResult, typeof(Error<string>));
        Assert.IsInstanceOfType(lambdaErrorResult, typeof(IResult<string>));
    }

}

