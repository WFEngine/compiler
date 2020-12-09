using System;
using System.IO;

namespace WFEngine.Compiler.Test.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string workflowsDirectory = $"{currentDirectory}/workflows.json";
            if (!File.Exists(workflowsDirectory))
                return;
            IWFCompiler compiler = new WFCompiler();
            Guid projectUUID = Guid.Parse("b40411ea-8eb5-40d6-8100-7c54d6fa9904");
            Guid workflowUUID = Guid.Parse("eb6e253e-375b-44e6-92ff-a02c52f42973");
            compiler.CompileConsoleApp(workflowsDirectory,projectUUID,workflowUUID);
            Console.ReadKey();
        }
    }
}
