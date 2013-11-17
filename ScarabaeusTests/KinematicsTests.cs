using FluentAssertions;

namespace ScarabaeusTests
{
    using System.Windows.Media.Media3D;

    using NUnit.Framework;

    using Scarabaeus;

    [TestFixture]
    public class KinematicsTests
    {
        const float Tolerance = 0.1f;

        [Test]
        public void SimpleInitialize()
        {
            var robot = new Scarabaeus();

            Assert.AreEqual(new Point3D(), robot.Center);

            Assert.AreEqual(90, robot.LeftLegs.Front.Coxa.Angle);
            Assert.AreEqual(15, robot.LeftLegs.Front.Coxa.Lenght);
            Assert.AreEqual(90, robot.LeftLegs.Front.Femur.Angle);
            Assert.AreEqual(100, robot.LeftLegs.Front.Femur.Lenght);
            Assert.AreEqual(90, robot.LeftLegs.Front.Tibia.Angle);
            Assert.AreEqual(120, robot.LeftLegs.Front.Tibia.Lenght);

            Assert.AreEqual(new Point3D(), robot.LeftLegs.Front.Position);

            Assert.AreEqual(90, robot.LeftLegs.Back.Coxa.Angle);
            Assert.AreEqual(90, robot.LeftLegs.Back.Femur.Angle);
            Assert.AreEqual(90, robot.LeftLegs.Back.Tibia.Angle);

            Assert.AreEqual(90, robot.RightLegs.Front.Coxa.Angle);
            Assert.AreEqual(90, robot.RightLegs.Front.Femur.Angle);
            Assert.AreEqual(90, robot.RightLegs.Front.Tibia.Angle);

            Assert.AreEqual(90, robot.RightLegs.Back.Coxa.Angle);
            Assert.AreEqual(90, robot.RightLegs.Back.Femur.Angle);
            Assert.AreEqual(90, robot.RightLegs.Back.Tibia.Angle);
        }

        [Test]
        public void CoxaAngleTest()
        {
            var robot = new Scarabaeus();
            var leg = robot.LeftLegs.Front;

            var newPoint = new Point3D(100, 0, 0);
            leg.CalculateAngles(newPoint);
            leg.Coxa.Angle.Should().BeApproximately(90, Tolerance);

            newPoint = new Point3D(0, 100, 0);
            leg.CalculateAngles(newPoint);
            leg.Coxa.Angle.Should().BeApproximately(180, Tolerance);

            newPoint = new Point3D(0, -100, 0);
            leg.CalculateAngles(newPoint);
            leg.Coxa.Angle.Should().BeApproximately(0, Tolerance);

            newPoint = new Point3D(50, -50, 0);
            leg.CalculateAngles(newPoint);
            leg.Coxa.Angle.Should().BeApproximately(45, Tolerance);

            newPoint = new Point3D(50, 50, 0);
            leg.CalculateAngles(newPoint);
            leg.Coxa.Angle.Should().BeApproximately(135, Tolerance);
        }

        [Test]
        public void FemurAngleTest()
        {
            var robot = new Scarabaeus();
            var leg = robot.LeftLegs.Front;

            var newPoint = new Point3D(100, 0, 0);
            leg.CalculateAngles(newPoint);
            leg.Femur.Angle.Should().BeApproximately(90, Tolerance);
        }

        [Test]
        public void TibiaAngleTest()
        {
            var robot = new Scarabaeus();
            var leg = robot.LeftLegs.Front;

            var newPoint = new Point3D(100, 0, 0);
            leg.CalculateAngles(newPoint);
            leg.Tibia.Angle.Should().BeApproximately(90, Tolerance);
        }

        [Test]
        public void CalcCirclesTest()
        {
            var robot = new Scarabaeus();
            var leg = robot.LeftLegs.Front;

            var newEnd = new Point3D(0, 100, 0);
            var result = leg.GetCirclesX(newEnd);
            
            //result.X
        }

        [Test]
        public void RotationMatrixTests()
        {
            var rotation = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 90));
            var result = rotation.Transform(new Point3D(0, 10, 0));

            result.X.Should().BeApproximately(-10, Tolerance);
            result.Y.Should().BeApproximately(0, Tolerance);

            var vector = rotation.Transform(new Vector3D(0, 10, 0));

            vector.X.Should().BeApproximately(-10, Tolerance);
            vector.Y.Should().BeApproximately(0, Tolerance);
            vector.Length.Should().BeApproximately(10, Tolerance);
            Vector3D.AngleBetween(vector, new Vector3D(0, 10, 0)).Should().BeApproximately(90, Tolerance);
        }

        [Test]
        public void CanReachTest()
        {
            var robot = new Scarabaeus();
            var leg = robot.LeftLegs.Front;

            var newEnd = new Point3D(10, 0, 0);
            Assert.True(leg.CanReach(newEnd));

            newEnd = new Point3D(300, 0, 0);
            Assert.False(leg.CanReach(newEnd));
        }
    }
}
