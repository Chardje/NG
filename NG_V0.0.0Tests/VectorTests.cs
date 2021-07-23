using Microsoft.VisualStudio.TestTools.UnitTesting;
using NG_V0._0._0;
using System;
using System.Collections.Generic;
using System.Text;

namespace NG_V0._0._0.Tests
{
    [TestClass()]
    public class VectorTests
    {
        [DataTestMethod]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 0, 0, 1)]
        [DataRow(0, 1, 0, 1)]
        [DataRow(0, 0, 1, 1)]
        [DataRow(-100, 0, 0, 100)]
        [DataRow(0, -200, 0, 200)]
        [DataRow(0, 0, -300, 300)]
        [DataRow(3, -4, 0, 5)]
        [DataRow(0, -15, 20, 25)]
        [DataRow(-8, 0, 6, 10)]
        [DataRow(1, -1, 0, 1.414213)] // sqrt(2) = 1.414213
        public void LengthTest(double x, double y, double z, double length)
        {
            double actual = new Vector(x, y, z).Length;
            Assert.AreEqual(length, actual, 1e-6);
        }

        [DataTestMethod]
        [DataRow(1, 0, 0, 1, 0, 0)]
        [DataRow(0, 1, 0, 0, 1, 0)]
        [DataRow(0, 0, 1, 0, 0, 1)]
        [DataRow(-1, 0, 0, -1, 0, 0)]
        [DataRow(0, -1, 0, 0, -1, 0)]
        [DataRow(0, 0, -1, 0, 0, -1)]
        [DataRow(10, 0, 0, 1, 0, 0)]
        [DataRow(0, 20, 0, 0, 1, 0)]
        [DataRow(0, 0, 30, 0, 0, 1)]
        [DataRow(-40, 0, 0, -1, 0, 0)]
        [DataRow(0, -50, 0, 0, -1, 0)]
        [DataRow(0, 0, -60, 0, 0, -1)]
        [DataRow(1, 1, 1, 0.57735, 0.57735, 0.57735)] // 1/sqrt(3) = 0.57735
        public void NormTest(double x, double y, double z, double xn, double yn, double zn)
        {
            Vector norm = new Vector(x, y, z).Norm;
            Assert.IsTrue(AreAlmostEqual(xn, norm.X, 1e-6) && AreAlmostEqual(yn, norm.Y, 1e-6) && AreAlmostEqual(zn, norm.Z, 1e-6), $"Expected ({xn}, {yn}, {zn}), Actual ({norm.X}, {norm.Y}, {norm.Z})");
        }

        [DataTestMethod]
        [DataRow(1, 0, 0, 1, 0, 0, 1)]
        [DataRow(0, 3, 0, 0, 2, 0, 6)]
        [DataRow(0, 0, -3, 0, 0, -4, 12)]
        [DataRow(0, 0, 3, 0, 0, -4, -12)]
        [DataRow(1, 0, 0, 0, 1, 0, 0)]
        public void ScalarProductTest(double x0, double y0, double z0, double x1, double y1, double z1, double product)
        {
            double actual = new Vector(x0, y0, z0) * new Vector(x1, y1, z1);
            Assert.AreEqual(product, actual, 1e-6);
        }


        private static bool AreAlmostEqual(double a, double b, double e)
        {
            return System.Math.Abs(a - b) < e;
        }
    }
}