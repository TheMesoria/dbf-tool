using System;
using System.IO;
using Mesoria.DbfTool.Utils;


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
            var dbf = DatabaseFileDeserializer.Load(stream);
        }
    }
}