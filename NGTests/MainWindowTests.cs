using Microsoft.VisualStudio.TestTools.UnitTesting;
using NG;
using NG.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using static NG.MainWindow;

namespace NG.Tests
{
    [STATestClass]
    public class MainWindowTests
    {
        /*
        [DataTestMethod]
        //[DataRow(100, 200, 20,  74, 200, 30, false, "1 pixel gap left")]
        [DataRow(100, 200, 20,  75, 200, 30, true, "still touches left")]
        [DataRow(100, 200, 20, 100, 200, 30, true, "same centers")]
        [DataRow(100, 200, 20, 125, 200, 30, true, "still touches right")]
        //[DataRow(100, 200, 20, 126, 200, 30, false, "1 pixel gap right")]
        public void TestCollideEllipseAndEllipse(int x0, int y0, int d0, int x1, int y1, int d1, bool collide, string comment)
        {
            EllipseF e0 = new EllipseF(x0, y0, d0);
            EllipseF e1 = new EllipseF(x1, y1, d1);
            Assert.AreEqual(expected: collide, actual: Collide(e0, e1));
        }*/
    }
}