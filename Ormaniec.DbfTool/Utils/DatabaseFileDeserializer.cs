using System;
using System.IO;
using System.Linq;
using Mesoria.DbfTool.Model;

namespace Mesoria.DbfTool.Utils
{
    public class DatabaseFileDeserializer
    {
        public static DatabaseFile Load(FileStream stream)
        {
            DatabaseFile databaseFile = new DatabaseFile();
            databaseFile.Header = LoadHeader(stream);
            databaseFile.Table = LoadTable(stream);

            return databaseFile;
        }

        /**
         *
         * 00h /   0| Version number      *1|  ^
         *          |-----------------------|  |
         * 01h /   1| Date of last update   |  |
         * 02h /   2|      YYMMDD        *21|  |
         * 03h /   3|                    *14|  |
         *          |-----------------------|  |
         * 04h /   4| Number of records     | Record
         * 05h /   5| in data file          | header
         * 06h /   6| ( 32 bits )        *14|  |
         * 07h /   7|                       |  |
         *          |-----------------------|  |
         * 08h /   8| Length of header   *14|  |
         * 09h /   9| structure ( 16 bits ) |  |
         *          |-----------------------|  |
         * 0Ah /  10| Length of each record |  |
         * 0Bh /  11| ( 16 bits )     *2 *14|  |
         *          |-----------------------|  |
         * 0Ch /  12| ( Reserved )        *3|  |
         * 0Dh /  13|                       |  |
         *          |-----------------------|  |
         * 0Eh /  14| Incomplete transac.*12|  |
         *          |-----------------------|  |
         * 0Fh /  15| Encryption flag    *13|  |
         *          |-----------------------|  |
         * 10h /  16| Free record thread    |  |
         * 11h /  17| (reserved for LAN     |  |
         * 12h /  18|  only )               |  |
         * 13h /  19|                       |  |
         *          |-----------------------|  |
         * 14h /  20| ( Reserved for        |  |
         *          |   multi-user dBASE )  |  |
         *          : ( dBASE III+ - )      :  |
         *          :                       :  |
         * 1Bh /  27|                       |  |
         *          |-----------------------|  |
         * 1Ch /  28| MDX flag (dBASE IV)*14|  |
         *          |-----------------------|  |
         * 1Dh /  29| Language driver     *5|  |
         *          |-----------------------|  |
         * 1Eh /  30| ( Reserved )          |  |
         * 1Fh /  31|                     *3|  |
         */
        private static Header LoadHeader(FileStream stream)
        {
            Header header = new Header();

            // Version number           | 8 bits | 1 byte  |
            header.Version = Convert.ToByte(stream.ReadByte());

            // Date of last update      | 24 bits | 3 bytes |
            var year = stream.ReadByte();
            var month = stream.ReadByte();
            var day = stream.ReadByte();
            header.Date = new DateTime(year + 2000, month, day);

            {
                // Number of records        | 32 bits | 4 bytes |
                var buffer = new Byte[4];
                stream.Read(buffer, 0, 4);
                header.RecordCount = BitConverter.ToUInt32(FixEndianness(buffer));
            }

            {
                // Header length            | 16 bits | 2 bytes |
                var buffer = new Byte[2];
                stream.Read(buffer, 0, 2);
                header.HeaderLength = BitConverter.ToUInt16(FixEndianness(buffer));
            }

            {
                // Record length            | 16 bits | 2 bytes |
                var buffer = new Byte[2];
                stream.Read(buffer, 0, 2);
                header.RecordLength = BitConverter.ToUInt16(FixEndianness(buffer));
            }

            {
                // Reserved                 | 16 bits | 2 bytes |
                var buffer = new Byte[2];
                stream.Read(buffer, 0, 2);
                header.ReservedRecord[0] = BitConverter.ToUInt16(FixEndianness(buffer));
            }

            // Incomplete transaction   |  8 bits | 1 byte  |
            var isStarted = Convert.ToBoolean(stream.ReadByte());
            header.IsTransactionStarted = isStarted;

            // Encryption flag          |  8 bits | 1 byte  |
            var isEncrypted = Convert.ToBoolean(stream.ReadByte());
            header.IsTransactionEncrypted = isEncrypted;

            {
                // Free record thread       | 32 bits | 4 bytes |
                var buffer = new Byte[4];
                stream.Read(buffer, 0, 4);
                header.FreeRecordThread = BitConverter.ToUInt32(FixEndianness(buffer));
            }

            {
                // Reserved for multi-user  | 64 bits | 8 bytes |
                var buffer = new Byte[8];
                stream.Read(buffer, 0, 8);
                header.MultiUserThread = BitConverter.ToUInt64(FixEndianness(buffer));
            }

            // MDX flag                 |  8 bits | 1 byte  |
            var isMdxAvailable = Convert.ToBoolean(stream.ReadByte());
            header.IsMdxAvailable = isMdxAvailable;

            // Language driver          |  8 bits | 1 byte  |
            var languageDriver = Convert.ToByte(stream.ReadByte());
            header.LanguageDriver = languageDriver;

            {
                // Reserved                 | 16 bits | 2 bytes |
                var buffer = new Byte[2];
                stream.Read(buffer, 0, 2);
                header.ReservedRecord[1] = BitConverter.ToUInt16(FixEndianness(buffer));
            }

            return header;
        }

        private static Table LoadTable(FileStream stream)
        {
            return null;
        }

        // private static Record LoadTableRecord(FileStream stream)
        // {
        //     return null;
        // }

        private static Byte[] FixEndianness(Byte[] buffer)
        {
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(buffer);
            return buffer;
        }
    }
}