using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTests;
using LubyTransform.Decode;
using EquationSolver;
using EquationSolver.Contracts;
using EquationSolver.Implementation;

namespace UnitTest
{
    [TestClass()]
    public class MatrixSolverTest
    {
        [TestMethod()]
        public void SolveTest()
        {
            //Arrange
            IMatrixSolver target = new MatrixSolver();
            var matrix = new int[7, 7] 
            {
                {1,0,1,0,0,0,2},
                {0,1,0,1,0,0,3},

                {1,0,0,0,1,0,4},
                {0,1,0,0,0,1,8},

                {0,0,1,0,1,0,6},
                {1,0,0,0,0,0,5},
                {0,1,0,0,0,0,10}
            };

            var expected = new int[7, 7] 
            {
                {1,0,0,0,0,0,5},
                {0,1,0,0,0,0,10},
                {0,0,1,0,0,0,7},
                {0,0,0,1,0,0,9},
                {0,0,0,0,1,0,1},
                {0,0,0,0,0,1,2},
                {0,0,0,0,0,0,0}
            };

            //Act
            var result = target.Solve(matrix);

            //Assert
            Assert.IsTrue(result, "couldnt solve matrix");
            Common.CheckArraysAreEqual(matrix, expected);
        }

    }
}