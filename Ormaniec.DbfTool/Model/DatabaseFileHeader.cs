using System;
using System.IO;
using System.Linq;

namespace Mesoria.DbfTool.Model
{
    public class Header
    {
        public Header(FileStream stream)
        {
            stream.Read(_dataRead, 0, 0x30);
        }

        private Byte[] _dataRead = new Byte[0x30];

        private readonly Tuple<Byte, Byte> _version = new(0x0, 1);
        private readonly Tuple<Byte, Byte> _year = new(0x1, 1);
        private readonly Tuple<Byte, Byte> _month = new(0x2, 1);
        private readonly Tuple<Byte, Byte> _day = new(0x3, 1);
        private readonly Tuple<Byte, Byte> _recordCount = new(0x4, 4);
        private readonly Tuple<Byte, Byte> _headerLength = new(0x8, 2);
        private readonly Tuple<Byte, Byte> _recordLength = new(0xA, 2);
        private readonly Tuple<Byte, Byte> _languageDriver = new(0x1D, 1);

        public Byte Version
        {
            get => _dataRead[_version.Item1];
            set => _dataRead[_version.Item1] = value;
        }

        public UInt32 Year
        {
            get => _dataRead[_year.Item1] + 2000u;
            set => _dataRead[_year.Item1] = (Byte) (value - 2000u);
        }

        public Byte Month
        {
            get => _dataRead[_month.Item1];
            set => _dataRead[_month.Item1] = value;
        }

        public Byte Day
        {
            get => _dataRead[_day.Item1];
            set => _dataRead[_day.Item1] = value;
        }

        public DateTime Date
        {
            get => new((int)Year, Month, Day);
            set
            {
                Year = (UInt32) value.Year;
                Month = (Byte) value.Month;
                Day = (Byte) value.Day;
            }
        }

        public UInt32 RecordCount
        {
            get => BitConverter.ToUInt32(_dataRead, _recordCount.Item1);
            set => Array.Copy(BitConverter.GetBytes(value), 0, _dataRead, _recordCount.Item1, _recordCount.Item2);
        }

        public UInt16 HeaderLength
        {
            get => BitConverter.ToUInt16(_dataRead, _headerLength.Item1);
            set => Array.Copy(BitConverter.GetBytes(value), 0, _dataRead, _headerLength.Item1, _headerLength.Item2);
        }

        public UInt16 RecordLength
        {
            get => BitConverter.ToUInt16(_dataRead, _recordLength.Item1);
            set => Array.Copy(BitConverter.GetBytes(value), 0, _dataRead, _recordLength.Item1, _recordLength.Item2);
        }

        public Byte LanguageDriver
        {
            get => _dataRead[_languageDriver.Item1];
            set => _dataRead[_languageDriver.Item1] = value;
        }
    }
}