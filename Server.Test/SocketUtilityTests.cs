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

        [Test]
        public void MsgReceive_WhenStreamCanRead_ReturnsReceivedMsg()
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
        public void MessageReceive_WhenStreamCannotRead_ReturnsEmptyString()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(s => s.CanRead).Returns(false);

            string receivedMsg = SocketUtility.MsgReceive(mockStream.Object);

            Assert.That(receivedMsg, Is.EqualTo(string.Empty));
        }

        [Test]
        public void MsgReceive_WhenExceptionThrown_ReturnsErrorMessage()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(s => s.CanRead).Returns(true);
            mockStream.Setup(s => s.Read(It.IsAny<byte[]>(), 0, 1024)).Throws(new IOException());

            string receivedMsg = SocketUtility.MsgReceive(mockStream.Object);

            Assert.That(receivedMsg, Is.EqualTo(string.Empty));
        }

    }
}
