using System;

namespace WFEngine.Compiler.Test.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            WFRunAtConsole runAtConsole = new WFRunAtConsole("workflows.json", "b40411ea-8eb5-40d6-8100-7c54d6fa9904", "09f11f7d-ffaf-409c-aa53-556aa4cc982e");
        }
    }
}
