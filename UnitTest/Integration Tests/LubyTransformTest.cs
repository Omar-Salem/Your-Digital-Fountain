using LubyTransform.Decode;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities;
using System.Collections.Generic;
using System.Text;
using EquationSolver;
using LubyTransform.Encode;
using EquationSolver.Contracts;
using EquationSolver.Implementation;

namespace UnitTests
{
    [TestClass()]
    public class LubyTransformTest
    {
        [TestMethod()]
        public void Encode_DecodeTest()
        {
            //Arrange
            string sentMessage = "Message";
            byte[] file = Encoding.ASCII.GetBytes(sentMessage);
            IEncode encoder = new Encode(file);
            var blocksCount = encoder.NumberOfBlocks;
            var overHead = 20;
            IList<Drop> drops = new List<Drop>();
            for (int i = 0; i < blocksCount + overHead; i++)
            {
                var drop = encoder.Encode();
                drops.Add(drop);
            }
            IMatrixSolver matrixSolver = new MatrixSolver();
            IDecode target = new Decode(matrixSolver);

            //Act
            var actualByte = target.Decode(drops, blocksCount, encoder.ChunkSize, encoder.FileSize);
            var receievdMessage = Encoding.ASCII.GetString(actualByte);

            //Assert
            Assert.AreEqual(sentMessage, receievdMessage);
        }
    }
}