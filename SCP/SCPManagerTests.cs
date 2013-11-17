using System.Threading;
using NUnit.Framework;

namespace TVA.Scarabaeus.SCP
{
    [TestFixture]
    public class SCPManagerTests
    {
        [Test]
        public void Send()
        {
            Assert.Inconclusive();

            var manager = new SCPManager();
            manager.Send(5, 10, 15);

            Thread.Sleep(1000);

            manager.Send(90, 180, 90);

            Thread.Sleep(1500);

            manager.Send(0, 90, 90);
        }
    }
}