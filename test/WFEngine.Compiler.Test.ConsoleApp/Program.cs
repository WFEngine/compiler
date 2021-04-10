using System;

namespace WFEngine.Compiler.Test.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WFRunAtConsole runAtConsole = new WFRunAtConsole(@"C:\Projects\wfengine\compiler\test\WFEngine.Compiler.Test.ConsoleApp\workflows.json", "b40411ea-8eb5-40d6-8100-7c54d6fa9904", "e5e991d8-714f-436b-b9dc-776df24cbd48");
        }
    }
}
