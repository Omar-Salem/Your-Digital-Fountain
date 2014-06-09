// -----------------------------------------------------------------------
// <copyright file="Common.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.IO;
    using System.Drawing.Imaging;

    public class Common
    {
        internal static void CheckArraysAreEqual(int[,] expected, int[,] actual)
        {
            Assert.AreEqual(expected.GetLength(0), actual.GetLength(0), "Rows Count are not the same");
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1), "Columns are not the same");

            for (int i = 0; i < actual.GetLength(0); i++)
            {
                for (int j = 0; j < actual.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}
