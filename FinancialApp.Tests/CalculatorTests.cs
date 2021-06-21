using System;
using Xunit;

namespace FinancialApp.Tests
{
    public class CalculatorTests
    {
        //[Fact]
        [Theory]
        [InlineData(2,3)]
        [InlineData(7,6)]
        [InlineData(4,5)]
        [InlineData(2,2)]
        public void Add_ValidValues_ReturnsSuccess(int a,int b)
        {
            //arrange 
            var calculator = new Calculator();
            //int a = 4;
            //int b = 6;

            //act
            var result = calculator.Add(a,b);

            //assert
            Assert.True((a+b)*0.5 == result);
            Assert.Equal((a+b)*0.5,result);

        }

        [Fact]
        public void Divide_ValidValues_ReturnsSuccess()
        {
            //arrange 
            var calculator = new Calculator();
            int a = 10;
            int b = 5;

            //act
            var result = calculator.Divide(a,b);

            //assert
            Assert.True(a/b == result);
            Assert.Equal(a/b,result);
        }  
        
        [Fact]
        public void Divide_ByZero_ReturnsException()
        {
            //arrange 
            var calculator = new Calculator();
            int a = 10;
            int b = 0;

            //act
            //var result = calculator.Divide(a,b);

            //assert
            Assert.Throws<ArgumentException>(() => calculator.Divide(a, b));
        }
    }
}
