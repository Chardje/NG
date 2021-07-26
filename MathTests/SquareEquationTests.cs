using Microsoft.VisualStudio.TestTools.UnitTesting;
using NG.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace NG.Math.Tests
{
    [TestClass]
    public class SquareEquationTests
    {
        [TestMethod]
        public void TwoRoots()
        {
            double x0;
            double x1;
            bool r = SquareEquation.Solve(1, -25, 150, out x0, out x1);
            Assert.IsTrue(r);
            Assert.AreEqual(10, x0);
            Assert.AreEqual(15, x1);
        }

        [TestMethod]
        public void OneRoot()
        {
            double x0;
            double x1;
            bool r = SquareEquation.Solve(1, -2, 1, out x0, out x1);
            Assert.IsTrue(r);
            Assert.AreEqual(1, x0);
            Assert.AreEqual(1, x1);
        }

        [TestMethod]
        public void NoRoots()
        {
            double x0;
            double x1;
            bool r = SquareEquation.Solve(1, -2, 2, out x0, out x1);
            Assert.IsFalse(r);
            Assert.AreEqual(double.NaN, x0);
            Assert.AreEqual(double.NaN, x1);
        }

        [TestMethod]
        public void TestNaN()
        {
            double x0;
            double x1;
            try
            {
                SquareEquation.Solve(1, -2, double.NaN, out x0, out x1);
                Assert.Fail("hahaah");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}