using System;

namespace WFEngine.Compiler
{
    public interface IWFCompiler
    {
        string CompileConsoleApp(string WFFile, Guid ProjectUID, Guid WorkflowUID);
    }
}
