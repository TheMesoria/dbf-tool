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
            databaseFile.Header = new Header(stream);
            databaseFile.Table = LoadTable(stream);

            return databaseFile;
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