using System;
using System.IO;
using System.Reflection;

namespace WFEngine.Compiler.Test.ConsoleApp
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {          
            WFRunAtConsole runAtConsole = new WFRunAtConsole("workflows.json", "b40411ea-8eb5-40d6-8100-7c54d6fa9904", "eb6e253e-375b-44e6-92ff-a02c52f42973");
        }
    }
}
