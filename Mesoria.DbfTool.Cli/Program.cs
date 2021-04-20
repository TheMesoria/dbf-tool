using System;
using System.IO;

namespace Mesoria.DbfTool.Cli
{
    class Program
    {
        private static String PathToDbf { get; set; }

        static void Main(string[] args)
        {
            // ToDo: Create class to handle input Params
            // Bug: If there is no args, hard crash occurs
            PathToDbf = args[1];

            FileStream stream = new FileStream(PathToDbf, FileMode.Open);
            Byte[] version = new byte[2];
            Byte[] year = new byte[2];
            Byte[] month = new byte[2];
            Byte[] day = new byte[2];
            Byte[] recordCount = new byte[4];

            stream.Read(version, 0, 1);
            stream.Read(year, 0, 1);
            stream.Read(month, 0, 1);
            stream.Read(day, 0, 1);
            stream.Read(recordCount, 0, 4);

            var Version = BitConverter.ToUInt16(version);
            var Year = BitConverter.ToUInt16(version) + 2000;
            var Month = BitConverter.ToUInt16(month);
            var Day = BitConverter.ToUInt16(day);
            var LineCount = BitConverter.ToUInt32(recordCount, 4);

            Console.WriteLine($"This dbf is from {Year}.{Month}.{Day} and have {LineCount} lines. Version[{Version}].");
        }
    }
}