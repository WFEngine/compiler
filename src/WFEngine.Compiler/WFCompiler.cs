using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using WFEngine.Activities.Core.Enums;
using WFEngine.Activities.Core.Helper;
using WFEngine.Activities.Core.Model;

namespace WFEngine.Compiler
{
    public class WFCompiler : IWFCompiler
    {
        public void RunWorkflow(string WFFile, Guid ProjectUID, Guid WorkflowUID)
        {            
            try
            {
                var workflowFileContent = File.ReadAllText(WFFile);
                WFModel model = JsonConvert.DeserializeObject<WFModel>(workflowFileContent);
                WFProject currentProject = model.Solution.Projects.FirstOrDefault(x => x.UniqueKey == ProjectUID);
                if (currentProject == null)
                    throw new ArgumentNullException(nameof(currentProject));
                if (currentProject.ProjectType == enumProjectType.Console.ToString())
                {
                    WFWorkflow currentFlow = currentProject.Workflows.FirstOrDefault(x => x.UniqueKey == WorkflowUID);
                    if (currentFlow == null)
                        throw new ArgumentNullException(nameof(currentFlow));
                    foreach (var block in currentFlow.Blocks)
                    {
                        if(!String.IsNullOrEmpty(block.AssemblyName) && block.IsContainer)
                        {
                            block.RunBlock(block.Variables);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
