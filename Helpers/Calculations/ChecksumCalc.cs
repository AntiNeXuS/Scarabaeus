using System.Linq;

namespace TVA.Scarabaeus.Helpers.Calculations
{
    public static class ChecksumCalc
    {
        private const byte poly = 0xD5;
        static byte[] table = new byte[256];

        static ChecksumCalc()
        {
            for (int i = 0; i < 256; ++i)
            {
                int temp = i;
                for (int j = 0; j < 8; ++j)
                {
                    if ((temp & 0x80) != 0)
                    {
                        temp = (temp << 1) ^ poly;
                    }
                    else
                    {
                        temp <<= 1;
                    }
                }
                table[i] = (byte) temp;
            }
        }

        public static byte Simple(byte[] data)
        {
            return Simple(data, (byte) (data.Count() - 1));
        }

        public static byte Simple(byte[] data, byte size)
        {
            byte checksum = 0;
            for (int i = 0; i <= size; i++)
                checksum += data[i];

            return (byte) (-checksum);
        }

        public static byte CRC8(byte[] data)
        {
            byte crc = 0;
            foreach (byte b in data)
            {
                crc = table[crc ^ b];
            }

            return crc;
        }
    }
}