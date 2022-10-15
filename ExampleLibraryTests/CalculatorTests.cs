using ExampleLibrary;
using FluentAssertions;

namespace ExampleLibraryTests;

public class CalculatorTests
{

    [TestCase(1,0,1)]
    [TestCase(0,1,1)]
    public void SimpleTestsScenario(int a, int b, int expectedResult)
    {
        //arrange
        var sut = new Calculator();

        //act
        var result = sut.Add(a, b);
        
        //assert
        result.Should().Be(expectedResult);
    }
}