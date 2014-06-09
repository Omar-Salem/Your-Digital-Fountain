using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using LubyTransform.Encode;

namespace UnitTests
{
    [TestClass()]
    public class EncodeTest
    {
        readonly byte[] file;
        readonly IEncode encoder;

        public EncodeTest()
        {
            file = new byte[4] { 7, 0, 2, 0 };
            encoder = new Encode(file);
        }

        #region Test Methods

        [TestMethod()]
        public void XOROperationTest()
        {
            //Arrange
            var privateAccessor = new Encode_Accessor(file);
            int idx = 0;
            IList<int> selectedParts = new List<int> { 1, 2, 3 };
            IList<byte[]> blocks = new List<byte[]> 
            {
                new byte[4]{1,0,0,0},
                new byte[4]{1,0,0,0},
                new byte[4]{0,0,0,0},
                new byte[4]{1,0,0,0}
            };
            byte expected = 0;

            //Act
            byte actual = privateAccessor.XOROperation(idx, selectedParts, blocks);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateDropDataTest()
        {
            //Arrange
            var privateAccessor = new Encode_Accessor(file);
            IList<int> selectedParts = new List<int> { 0, 1, 2 };
            IList<byte[]> blocks = new List<byte[]> 
            {
                new byte[2]{0,0},
                new byte[2]{0,1},
                new byte[2]{1,0},
                new byte[2]{1,1}
            };
            byte[] expected = new byte[2] { 1, 1 };

            //Act
            byte[] actual = privateAccessor.CreateDropData(selectedParts, blocks, 2);

            //Assert
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        [TestMethod()]
        public void GetSelectedParts_ReturnsAllBlocksTest()
        {
            //Arrange
            var privateAccessor = new Encode_Accessor(file);
            var parts = new Dictionary<int, int>();

            //Act
            for (int i = 0; i < privateAccessor.blocks.Count() + 20; i++)
            {
                IList<int> actual = privateAccessor.GetSelectedParts();
                foreach (var item in actual)
                {
                    if (!parts.ContainsKey(item))
                    {
                        parts.Add(item, 0);
                    }
                }
            }

            //Assert
            Assert.AreEqual(privateAccessor.blocks.Count(), parts.Count);
        }

        #endregion
    }
}