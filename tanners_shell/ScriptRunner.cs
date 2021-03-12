using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Serilog;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using tanners_shell.views;

namespace tanners_shell
{
    static class ScriptRunner
    {
        private static CSharpCompilation compilation;

        static ScriptRunner()
        {
           var k = AppDomain.CurrentDomain.GetAssemblies();
            
            string assemblyName = Path.GetRandomFileName();
            AppDomain currentDomain = AppDomain.CurrentDomain;
            List<MetadataReference> metadataReferenceList = new List<MetadataReference>();
            Assembly[] assemblyArray = currentDomain.GetAssemblies();
            foreach (Assembly domainAssembly in assemblyArray)
            {
                try
                {
                    AssemblyMetadata assemblyMetadata = AssemblyMetadata.CreateFromFile(domainAssembly.Location);
                    MetadataReference metadataReference = assemblyMetadata.GetReference();
                    metadataReferenceList.Add(metadataReference);
                }
                catch (Exception e)
                {
                    Log.Debug("failed to get MetadataReference {0}", e.Message);
                }
            }

            metadataReferenceList.Add(AssemblyMetadata.CreateFromFile(Assembly.Load("Microsoft.CSharp").Location).GetReference());

            compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new List<SyntaxTree> { },
                references: metadataReferenceList,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        }

        public static bool runCommand(string command)
        {
            string script = getEmbeddedCommandScript();
            script = script.Replace("//command", command);

            Assembly assembly = compile(script);
            if (assembly == null)
            {
                return false;
            }
            
            MethodInfo run = assembly.GetType("CommandScript").GetMethod("run",
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.Static);

            try
            {
                run.Invoke(null, null);
            }
            catch(Exception ex)
            {
                History.add(new StringView(ex.Message));
                return false;
            }

            return true;
        }

        private static string getEmbeddedCommandScript()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "tanners_shell.CommandScript.cs";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        private static Assembly compile(string command)
        {
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(command);
            Compilation temp = compilation.AddSyntaxTrees(syntaxTree);
            
            using (var ms = new MemoryStream())
            {
                EmitResult result = temp.Emit(ms);
                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
                    foreach (Diagnostic diagnostic in failures)
                    {
                        History.add(new StringView(diagnostic.ToString()));
                    }

                    return null;
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    return Assembly.Load(ms.ToArray());
                }
            }
        }
    }
}
