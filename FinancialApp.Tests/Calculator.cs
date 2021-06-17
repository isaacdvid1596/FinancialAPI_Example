using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FinancialApp.Tests
{
    public class Calculator
    {
        public double Add(int a, int b) => (a + b) * 0.5;

        public int Subtract(int a, int b) => a - b;

        public int Multiply(int a, int b) => a * b;

        public int Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new ArgumentException("Denominator can't be 0");
            }
            return a / b;
        }

    }
}
