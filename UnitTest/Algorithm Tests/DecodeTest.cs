using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LubyTransform.Decode;
using Entities;
using EquationSolver;
using EquationSolver.Contracts;
using EquationSolver.Implementation;
using Moq;

namespace UnitTests
{
    [TestClass()]
    public class DecodeTest
    {
        [TestMethod()]
        public void BuildMatrixTest()
        {
            //Arrange
            IMatrixSolver matrixSolver = new MatrixSolver();
            Decode_Accessor target = new Decode_Accessor(matrixSolver);
            IList<Drop> drops = new Drop[3]
            {
                new Drop(){SelectedParts=new int[2]{0,1},Data=new byte[2]{1,2}},
                new Drop(){SelectedParts=new int[2]{0,2},Data=new byte[2]{3,4}},
                new Drop(){SelectedParts=new int[2]{1,2},Data=new byte[2]{5,6}}
            };
            var expected = new int[6, 7] 
            {
                {1,0,1,0,0,0,1},
                {0,1,0,1,0,0,2},

                {1,0,0,0,1,0,3},
                {0,1,0,0,0,1,4},

                {0,0,1,0,1,0,5},
                {0,0,0,1,0,1,6}
            };
            int blocksCount = 3;
            int chunkSize = 2;
            //Act
            var actual = target.BuildMatrix(drops, blocksCount, chunkSize);

            //Assert
            Common.CheckArraysAreEqual(expected, actual);
        }
    }
}