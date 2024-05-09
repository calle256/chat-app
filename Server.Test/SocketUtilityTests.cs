using System;
using ChatApp;
using System.Net.Sockets;
using System.Text;
using Moq;



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

    }
}
