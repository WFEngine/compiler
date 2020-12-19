using System;

namespace WFEngine.Compiler
{
    public interface IWFCompiler
    {
        void RunWorkflow(string WFFile, Guid ProjectUID, Guid WorkflowUID);
    }
}
