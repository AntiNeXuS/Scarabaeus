using NUnit.Framework;

namespace TVA.Scarabaeus.Helpers.Calculations
{
    [TestFixture]
    public class ChecksumCalcTests
    {
        [Test]
        public void Simple()
        {
            Assert.AreEqual(0x01, ChecksumCalc.Simple(new byte[] {0xFF}));

            Assert.AreEqual(0x07, ChecksumCalc.Simple(new byte[] {0xFF, 0xFA}));
        }

        [Test]
        public void CRC8()
        {
            var crc = ChecksumCalc.CRC8(new byte[] {0x01, 0x02, 0x03});
            var crc2 = ChecksumCalc.CRC8(new byte[] {0x01, 0x02, 0x03, crc});
            Assert.AreEqual(0, crc2);
        }
    }
}