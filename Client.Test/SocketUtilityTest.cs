using System.Text;
using ChatApp;
using Moq;
using System;
using System.Net.Sockets;


namespace Test
{
    [TestFixture]
    public class SocketUtilityTest
    {
        [Test]
        public void MsgSend_WhenStreamCanWrite_SendsMessage()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(s => s.CanWrite).Returns(true);
            string msg = "Test message";
            byte[] expectedBytes = Encoding.UTF8.GetBytes(msg);

            mockStream.Setup(s => s.Write(expectedBytes, 0, expectedBytes.Length)).Verifiable();

            SocketUtility.MsgSend(mockStream.Object, msg);

            mockStream.Verify();
        }

        [Test]
        public void MsgSend_WhenStreamCannotWrite_DoesNotSendMessage()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(s => s.CanWrite).Returns(false);
            string msg = "Test message";



            SocketUtility.MsgSend(mockStream.Object, msg);

            mockStream.Verify(s => s.Write(It.IsAny<byte[]>(), 0, It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void MsgReceive_WhenStreamCanRead_ReturnsReceivedMessage()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(s => s.CanRead).Returns(true);

            string msg = "Test message";
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);

            mockStream.Setup(s => s.Read(It.IsAny<byte[]>(), 0, 1024)).Returns(msgBytes.Length)
                      .Callback<byte[], int, int>((buffer, offset, count) => msgBytes.CopyTo(buffer, offset));

            string receivedMsg = SocketUtility.MsgReceive(mockStream.Object);

            Assert.That(receivedMsg, Is.EqualTo(msg));
        }

        [Test]
        public void MsgReceive_WhenStreamCannotRead_ReturnsEmptyString()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(s => s.CanRead).Returns(false);

            string receivedMsg = SocketUtility.MsgReceive(mockStream.Object);

            Assert.That(receivedMsg, Is.EqualTo(string.Empty));
        }

    }
}