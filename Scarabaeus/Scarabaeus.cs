namespace Scarabaeus
{
    using System.Windows.Media.Media3D;

    public class Scarabaeus
    {
        public Point3D Center;

        public Point3D WeightCenter;

        public LegSet LeftLegs;

        public LegSet RightLegs;

        public Scarabaeus()
        {
            LeftLegs = new LegSet();
            RightLegs = new LegSet();

            Center = new Point3D();
            WeightCenter = new Point3D();
        }

    }
}