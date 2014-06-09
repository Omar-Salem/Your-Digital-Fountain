using Client.Receiver;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using LubyTransform.Decode;
using System.Collections.Generic;
using Client.Receiver.EncodeService;
using Entities;

namespace UnitTest
{
    [TestClass()]
    public class DecoderViewModelTest
    {
        [TestMethod()]
        public void StartReceivingExecuteTest()
        {
            //Arrange
            Mock<IDecode> decodeMock = new Mock<IDecode>();
            Mock<IEncodeService> encodeServiceMock = new Mock<IEncodeService>();
            var drop = new Drop { Data = new byte[4] { 7, 0, 2, 0 }, SelectedParts = new int[2] { 1, 2 } };
            encodeServiceMock.Setup(m => m.Encode()).Returns(drop);
            DecoderViewModel decoderViewModel = new DecoderViewModel(decodeMock.Object, encodeServiceMock.Object);

            //Act
            decoderViewModel.StartReceiving.Execute(null);

            //Assert
            decodeMock.Verify(m => m.Decode(It.IsAny<IList<Entities.Drop>>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }
    }
}
