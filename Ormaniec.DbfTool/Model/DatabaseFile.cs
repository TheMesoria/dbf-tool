using System;

namespace Mesoria.DbfTool.Model
{
    public class Header
    {
        public Byte Version { get; set; }
        public DateTime Date { get; set; }
        public UInt32 RecordCount { get; set; }
        public UInt16 HeaderLength { get; set; }
        public UInt16 RecordLength { get; set; }
        public UInt16 ReservedRecord { get; set; }
        public bool IsTransactionStarted { get; set; }
        public bool IsTransactionEncrypted { get; set; }
        public UInt32 FreeRecordThread { get; set; }
        public UInt64 MultiUserThread { get; set; }
        public bool IsMdxAvailable { get; set; }    
        public Byte LanguageDriver { get; set; }
        public UInt16 ReservedRecordEnd { get; set; }
    }

    public class Record
    {
        
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