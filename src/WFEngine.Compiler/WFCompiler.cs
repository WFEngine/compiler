using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Loader;
using WFEngine.Activities.Core;
using WFEngine.Activities.Core.Enums;
using WFEngine.Activities.Core.Model;

namespace WFEngine.Compiler
{
    public class WFCompiler : IWFCompiler
    {
        public string CompileConsoleApp(string WFFile, Guid ProjectUID, Guid WorkflowUID)
        {
            string mainCode = @"          
                public static class WFItem{
                    public static void Run(){
                        {0}
                    }
                }            
            ";
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
                    string code = "";
                    code += CompileVariables(currentFlow);
                    foreach (var block in currentFlow.Blocks)
                    {
                        code += CompileBlock(block);
                    }
                    mainCode = mainCode.Replace("{0}", code);

                    var callingReference = Assembly.GetCallingAssembly();
                    var referenceAssembliesName = callingReference.GetReferencedAssemblies();
                    var referenceAssemblies = referenceAssembliesName.Select(x => Assembly.Load(x.FullName));

                    List<MetadataReference> metadataReferences = new List<MetadataReference>();
                    metadataReferences.Add(MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location));
                    metadataReferences.Add(MetadataReference.CreateFromFile(callingReference.Location));
                    foreach (var item in referenceAssemblies)
                    {
                        metadataReferences.Add(MetadataReference.CreateFromFile(item.Location));
                    }

                    var compilation = CSharpCompilation.Create("wfengine")
                    .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
                    .AddReferences(
                       metadataReferences.ToArray()
                    ).AddSyntaxTrees(CSharpSyntaxTree.ParseText(mainCode));


                    var fileName = "wfengine.dll";

                    var compilationResult = compilation.Emit(fileName);

                    var compilationAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(Path.GetFullPath(fileName));
                    compilationAssembly.GetType("WFItem").GetMethod("Run").Invoke(null, null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return mainCode;
        }
     
        private string CompileVariables(WFWorkflow workflow)
        {
            return "";
        }
        private string CompileBlock(WFBlock block)
        {
            var code = "";
            try
            {
                if (!String.IsNullOrEmpty(block.AssemblyName))
                {
                    Assembly assembly = Assembly.Load(block.AssemblyName);
                    IWFActivity activity = (IWFActivity)Activator.CreateInstance(Type.GetType($"{block.ActivityName}, {assembly.FullName}", true));
                    activity.SetArguments(block.Arguments);
                    return activity.CompileActivity();
                }
                if (block.Blocks.Any())
                {
                    foreach (var subBlocks in block.Blocks)
                    {
                        code += CompileBlock(subBlocks);
                    }
                }
                //return "";
            }
            catch (Exception ex)
            {
                throw;
            }
            return code;
        }
    }
}
