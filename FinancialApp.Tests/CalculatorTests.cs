using System;
using Xunit;

namespace FinancialApp.Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(2, 3)]
        [InlineData(7, 6)]
        [InlineData(4, 5)]
        [InlineData(2, 2)]
        public void Add_ValidValues_ReturnSuccess(int a, int b)
        {
            //arrange
            var calculator = new Calculator();

            //act
            var result = calculator.Add(a, b);

            //assert
            Assert.True((a + b)*0.5 == result);
            Assert.Equal((a + b) * 0.5, result);
        }

        [Fact]
        public void Divide_ValidValues_ReturnSuccess()
        {
            //arrange
            var calculator = new Calculator();
            int a = 4;
            int b = 6;

            //act
            var result = calculator.Divide(a, b);

            //assert
            Assert.True(a/b == result);
            Assert.Equal(a/b, result);
        }

        [Fact]
        public void Divide_ZeroAsDenominator_ReturnException()
        {
            //arrange
            var calculator = new Calculator();
            int a = 4;
            int b = 0;

            //assert
            Assert.Throws<ArgumentException>(() => calculator.Divide(a, b));
        }
    }
}
