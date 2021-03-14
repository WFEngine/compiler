using System;

namespace WFEngine.Compiler.Test.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WFRunAtConsole runAtConsole = new WFRunAtConsole(@"C:\Projects\wfengine\compiler\test\WFEngine.Compiler.Test.ConsoleApp\workflows.json", "b40411ea-8eb5-40d6-8100-7c54d6fa9904", "1bdab4bc-cac6-43b0-8df4-d636519f606c");
        }
    }
}
