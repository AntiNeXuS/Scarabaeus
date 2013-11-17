using System.IO.Ports;
using TVA.Scarabaeus.SCP.Packages;

namespace TVA.Scarabaeus.SCP
{
    public class SCPManager
    {
        private readonly SerialPort port;

        public SCPManager()
        {
            port = new SerialPort("COM5", 9600);
        }

        public void Send(byte coxaAngle, byte femurAngle, byte tibiaAngle)
        {
            var package = new SimplePackage(coxaAngle, femurAngle, tibiaAngle);

            port.Open();
            port.Write(package.ToString());
            port.Close();
        }
    }
}