using System;
using System.IO;

namespace WFEngine.Compiler
{
    public class WFRunAtConsole
    {
        public string FileName { get; set; }
        public Guid ProjectUUID { get; set; }
        public Guid WorkflowUUID { get; set; }

        public WFRunAtConsole(string FileName,string ProjectUUID,string WorkflowUUID)
        {
            this.FileName = FileName;
            this.ProjectUUID = Guid.Parse(ProjectUUID);
            this.WorkflowUUID = Guid.Parse(WorkflowUUID);

            Run();
        }

        private void Run()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string workflowsDirectory = $"{currentDirectory}/{FileName}";
            if (!File.Exists(workflowsDirectory))
                return;
            IWFCompiler compiler = new WFCompiler();
            compiler.RunWorkflow(workflowsDirectory, ProjectUUID, WorkflowUUID);
        }
    }
}
