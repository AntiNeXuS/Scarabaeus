namespace TVA.Scarabaeus.SCP.Packages
{
    public class SimplePackage
    {
        public byte CoxaAngle;
        public byte FemurAngle;
        public byte TibiaAngle;

        public SimplePackage(byte coxaAngle, byte femurAngle, byte tibiaAngle)
        {
            CoxaAngle = coxaAngle;
            FemurAngle = femurAngle;
            TibiaAngle = tibiaAngle;
        }

        public override string ToString()
        {
            return string.Format("S{0},{1},{2}E", CoxaAngle, FemurAngle, TibiaAngle);
        }
    }
}