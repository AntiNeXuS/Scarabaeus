using System;
using TVA.Scarabaeus.Helpers.Calculations;

namespace TVA.Scarabaeus.SCP.Packages
{
    public class Package : IEquatable<Package>
    {
        #region - Constants -

        /// <summary>
        /// Размер 
        /// </summary>
        public const byte AdditionalInfoSize = 5;

        #endregion

        #region - Package content -

        /// <summary>
        /// Синхронизирующий байт, признак начала пакета
        /// </summary>
        public const byte Sync = 0xAC;

        /// <summary>
        /// Идентификатор пакета
        /// </summary>
        public byte Id;

        /// <summary>
        /// Тип пакета
        /// </summary>
        public PackageType Type;

        /// <summary>
        /// Размер данных в пакете
        /// </summary>
        public byte DataSize;

        /// <summary>
        /// Данные
        /// </summary>
        public byte[] Data;

        /// <summary>
        /// Контрольная сумма
        /// </summary>
        public byte Checksum;

        #endregion 

        #region - Constructors -

        internal Package()
        {
        }

        /// <summary>
        /// Инициализация пакета данных
        /// </summary>
        /// <param name="id">Идентификатор пакета</param>
        /// <param name="type">Тип пакета</param>
        /// <param name="data">Данные</param>
        public Package(byte id, PackageType type, byte[] data)
        {
            Id = id;
            Type = type;
            if (data == null)
            {
                DataSize = 0;
            }
            else
            {
                DataSize = (byte) data.Length;
                Data = new byte[DataSize];
                for (int i = 0; i < DataSize; i++)
                {
                    Data[i] = data[i];
                }
            }
        }

        #endregion

        /// <summary>
        /// Упаковка данных в пакет с подсчетом контрольной суммы
        /// </summary>
        /// <returns></returns>
        public byte[] AsBytes()
        {
            var result = new byte[DataSize + AdditionalInfoSize];
            result[0] = Sync;
            result[1] = Id;
            result[2] = (byte) Type;
            result[3] = DataSize;
            for (byte i = 0; i < DataSize; i++)
            {
                result[4 + i] = Data[i];
            }

            result[DataSize + AdditionalInfoSize - 1] = ChecksumCalc.Simple(result, (byte) (DataSize + 4));

            return result;
        }

        /// <summary>
        /// Проба распаковки пакета из массива байт
        /// </summary>
        /// <param name="value">Массив байт</param>
        /// <param name="result">Пакет данных в случае успеха</param>
        /// <returns>Результат попытки распаковки пакета</returns>
        public static bool TryParseFromBytes(byte[] value, out Package result)
        {
            var index = Array.FindIndex(value, b => b == Sync);
            if (index == -1)
            {
                result = null;
                return false;
            }

            if (index + AdditionalInfoSize > value.Length)
            {
                result = null;
                return false;
            }

            result = new Package
            {
                Id = value[index + 1],
                Type = (PackageType) value[index + 2],
                DataSize = value[index + 3]
            };

            if (index + result.DataSize + AdditionalInfoSize > value.Length)
            {
                result = null;
                return false;
            }

            result.Data = new byte[result.DataSize];
            for (byte i = 0; i < result.DataSize; i++)
            {
                result.Data[i] = value[index + 4 + i];
            }

            result.Checksum = value[index + result.DataSize + AdditionalInfoSize - 1];

            return true;
        }

        /// <summary>
        /// Проверка контрольной суммы пакета
        /// </summary>
        public bool IsCorrect
        {
            get
            {
                return Checksum == AsBytes()[DataSize + AdditionalInfoSize - 1];
            }
        }

        public bool Equals(Package other)
        {
            if (Id != other.Id || Type != other.Type || DataSize != other.DataSize)
            {
                return false;
            }

            for (byte i = 0; i < DataSize; i++)
            {
                if (Data[i] != other.Data[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}