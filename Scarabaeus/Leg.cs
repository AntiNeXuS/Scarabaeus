namespace Scarabaeus
{
    using System.Windows.Media.Media3D;

    public class Leg
    {
        public LegPart Coxa;

        public LegPart Femur;
        
        public LegPart Tibia;

        public Leg(float coxaLenght = 15, float femurLenght = 100, float tibiaLenght = 120)
        {
            Coxa = new LegPart(coxaLenght);
            Femur = new LegPart(femurLenght);
            Tibia = new LegPart(tibiaLenght);
        }

        public Point3D Position
        {
            get
            {
                return _position;
            }
        }

        private Point3D _position;
    }
}