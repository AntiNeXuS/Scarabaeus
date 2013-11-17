using NUnit.Framework;

namespace TVA.Scarabaeus.SCP.Packages
{
    [TestFixture]
    public class PackageTests
    {
        [Test]
        public void AsByteTest()
        {
            var package = new Package(10, PackageType.SetupConnection, new byte[] {0x00, 0xFF, 0xAA});
            Assert.AreEqual(new byte[] {Package.Sync, 10, 3, 3, 0x00, 0xFF, 0xAA, 155}, package.AsBytes());

            package = new Package(10, PackageType.SetupConnection, null);
            Assert.AreEqual(new byte[] {Package.Sync, 10, 3, 0, 71}, package.AsBytes());
        }

        [Test]
        public void FromByteTest()
        {
            var bytes = new byte[] {Package.Sync, 10, 3, 3, 0x00, 0xFF, 0xAA, 155};
            var package = new Package(10, PackageType.SetupConnection, new byte[] { 0x00, 0xFF, 0xAA });
            Package result;
            Assert.IsTrue(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNotNull(result);
            Assert.AreEqual(package, result);

            bytes = new byte[] {0xfe, Package.Sync, 10, 3, 3, 0x00, 0xFF, 0xAA, 155 };
            package = new Package(10, PackageType.SetupConnection, new byte[] { 0x00, 0xFF, 0xAA });
            Assert.IsTrue(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNotNull(result);
            Assert.AreEqual(package, result);

            bytes = new byte[] { 0xfe, 0xff, 0x00, Package.Sync, 10, 3, 3, 0x00, 0xFF, 0xAA, 155 };
            package = new Package(10, PackageType.SetupConnection, new byte[] { 0x00, 0xFF, 0xAA });
            Assert.IsTrue(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNotNull(result);
            Assert.AreEqual(package, result);

            bytes = new byte[] {0xfe, 0xff, 0x99};
            Assert.IsFalse(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNull(result);

            bytes = new byte[] { 0xfe, Package.Sync, 10, 3, 4, 0x00, 0xFF, 0xAA, 155 };
            Assert.IsFalse(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNull(result);
        }

        [Test]
        public void ChecksumChecker()
        {
            var bytes = new byte[] { Package.Sync, 10, 3, 3, 0x00, 0xFF, 0xAA, 155 };
            Package result;
            Assert.IsTrue(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsCorrect);

            bytes = new byte[] { Package.Sync, 10, 3, 3, 0x00, 0xFF, 0xAA, 1 };
            Assert.IsTrue(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCorrect);

            bytes = new byte[] { Package.Sync, 10, 3, 3, 0x00, 0xFE, 0xAA, 155 };
            Assert.IsTrue(Package.TryParseFromBytes(bytes, out result));
            Assert.IsNotNull(result);
            Assert.IsFalse(result.IsCorrect);
        }
    }
}