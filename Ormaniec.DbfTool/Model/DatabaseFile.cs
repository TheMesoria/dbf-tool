using System;

namespace Mesoria.DbfTool.Model
{
    

    public class FieldDescriptor
    {
        public String Name { get; set; }
        public Char Type { get; set; }
        public UInt32 Offset { get; set; }
        public Byte Length { get; set; }
        public Byte DecimalCount { get; set; }
        public Byte WorkAreaId { get; set; }
        public Byte SetFieldFlag { get; set; }
        public Byte IndexFieldFlag { get; set; }

        public UInt16[] ReservedForMultiUserDBase { get; set; } = new UInt16[2];
    }

    public class Table
    {
    }

    public class DatabaseFile
    {
        public Header Header { get; set; }
        public Table Table { get; set; }
    }
}