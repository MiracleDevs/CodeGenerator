using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration;
using MiracleDevs.CodeGen.Logic.CodeGen.Output;
using MiracleDevs.CodeGen.Logic.Logging;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.UI.Console
{
    internal class Program
    {
        public class CodeGenOptions
        {
            [Option('f', "filename", HelpText = "Indicates the path of a output configuration json file.")]
            public string FileName { get; set; }

            [Option('d', "directory", HelpText = "Indicates a directory path in which all the output configuration files will be used to generate code.")]
            public string Folder { get; set; }

            [Option('t', "print-trans", HelpText = "Prints all the translations.")]
            public bool PrintTranslations { get; set; }

            [Option('o', "print-objdef", HelpText = "Prints all the object definitions.")]
            public bool PrintObjectDefinition { get; set; }

            [Option('h', "help", HelpText = "Prompts the help.")]
            public bool Help { get; set; }
        }

        private static void Main(string[] args)
        {
            var started = DateTime.Now;

            LoggingService.Instance = new ConsoleLoggingService();
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");
            LoggingService.Instance.Notice("Miracle Devs");
            LoggingService.Instance.Notice("Code Generation Tool");
            LoggingService.Instance.Notice("");
            LoggingService.Instance.Notice($"Started at: {started}");
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");

            var options = new CodeGenOptions();
            var currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (currentLocation == null)
            {
                LoggingService.Instance.Error("Couldn't retrieve the current location.");
                return;
            }

            if (!Parser.Default.ParseArguments(args, options))
            {
                LoggingService.Instance.Error("Couldn't parse the command line arguments.");
                return;
            }

            if (options.Help)
            {
                LoggingService.Instance.Write(HelpText.AutoBuild(options).ToString());
                return;
            }

            foreach (var fileName in GetFilesToProcess(options, currentLocation))
            {
                GenerateFile(fileName, options);
            }

            var ended = DateTime.Now;
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");
            LoggingService.Instance.Notice($"Ended at: {ended}");
            LoggingService.Instance.Notice($"Elapsed: { new TimeSpan(ended.Subtract(started).Ticks).TotalSeconds } sec");
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");
        }

        private static List<string> GetFilesToProcess(CodeGenOptions options, string currentLocation)
        {
            var files = new List<string>();

            if (!string.IsNullOrWhiteSpace(options.FileName))
                files.Add(options.FileName);
            else if (options.Folder != null)
                files.AddRange(Directory.EnumerateFiles(Path.Combine(currentLocation, options.Folder), "*.json"));
            return files;
        }

        private static void GenerateFile(string fileName, CodeGenOptions options)
        {
            LoggingService.Instance.Notice($"Opening file: [{fileName}]");

            // opens the output configuration.
            var outputConfigurations = OutputConfiguration.Open(fileName);

            // register a custom assembly resolver.
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += (s, e) => ResolveAssembly(outputConfigurations, e.Name);

            // loads the requested assembly, and extract the type.
            var assembly = Assembly.LoadFile(ResolvePath(outputConfigurations.Assembly));
            var type = assembly.ExportedTypes.FirstOrDefault(x => x.Name == outputConfigurations.EntryPointType);

            LoggingService.Instance.WriteLine("");
            LoggingService.Instance.WriteLine($"Processing type [{outputConfigurations.EntryPointType}]");

            // open the translation configuration and process the types.
            Translator.Translators.Open(outputConfigurations.Language);
            Translator.Definitions.ProcessEntryPointType(type);

            if (options.PrintTranslations)
            {
                LoggingService.Instance.WriteLine("Translators:");

                foreach (var translator in Translator.Translators.Translators.Values)
                {
                    LoggingService.Instance.WriteLine($"  - {translator.Name} to {translator.Translation}");
                }
            }

            if (options.PrintObjectDefinition)
            {
                LoggingService.Instance.WriteLine("Object Definitions:");

                foreach (var definition in Translator.Definitions.Definitions.Values)
                {
                    LoggingService.Instance.WriteLine($"  - {definition.Name}");
                }
            }

            LoggingService.Instance.WriteLine($"Processing finished [{outputConfigurations.EntryPointType}]");
            LoggingService.Instance.WriteLine("");
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");

            // runs the code generation code to produce the output.
            new CodeGenerator(outputConfigurations.Language).Generate(outputConfigurations);
        }

        private static Assembly ResolveAssembly(OutputConfiguration configuration, string name)
        {
            string assemblyName = null;

            try
            {
                var path = Path.GetDirectoryName(ResolvePath(configuration.Assembly));

                if (path == null)
                    throw new Exception("The assembly location couldn't be retrieved.");

                assemblyName = Path.Combine(path, name.Split(',').First() + ".dll");

                LoggingService.Instance.WriteLine("");
                LoggingService.Instance.Notice("Loading Assembly:");
                LoggingService.Instance.WriteLine($"{assemblyName}");
                
                return Assembly.LoadFile(assemblyName);
            }
            catch
            {
                LoggingService.Instance.Error($"Problems loading assembly [{assemblyName ?? name}]");
                return null;
            }
        }

        private static string ResolvePath(string path)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var fileName = Path.Combine(currentDirectory, path);
            return fileName;
        }
    }
}
