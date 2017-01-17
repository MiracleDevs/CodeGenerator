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
        private static CodeGenerator CodeGenerator { get; set; }

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

            var location = GetCurrentLocation();
            var options = GetOptions(args);

            if (PrintHelp(options))
                return;

            foreach (var fileName in GetFilesToProcess(options, location))
            {
                GenerateFile(fileName, options);
            }

            var ended = DateTime.Now;
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");
            LoggingService.Instance.Notice($"Ended at: {ended}");
            LoggingService.Instance.Notice($"Elapsed: { new TimeSpan(ended.Subtract(started).Ticks).TotalSeconds } sec");
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");
        }

        private static void GenerateFile(string fileName, CodeGenOptions options)
        {
            LoggingService.Instance.Notice($"Opening file: [{fileName}]");

            // opens the output configuration.
            var outputConfigurations = OutputConfiguration.Open(fileName);

            var type = GetTypeFromAssembly(outputConfigurations);

            LoggingService.Instance.WriteLine("");
            LoggingService.Instance.WriteLine($"Processing type [{outputConfigurations.EntryPointType}]");

            // open the translation configuration and process the types.
            Translator.Translators.Open(outputConfigurations.Language);
            Translator.Definitions.ProcessEntryPointType(type);

            PrintTranslations(options);
            PrintDefinitions(options);

            LoggingService.Instance.WriteLine($"Processing finished [{outputConfigurations.EntryPointType}]");
            LoggingService.Instance.WriteLine("");
            LoggingService.Instance.Notice("------------------------------------------------------------------------------------------------------");

            // runs the code generation code to produce the output.
            if (CodeGenerator == null || CodeGenerator.Language != outputConfigurations.Language)
                CodeGenerator = new CodeGenerator(outputConfigurations.Language);

            CodeGenerator.Generate(outputConfigurations);
        }

        private static Type GetTypeFromAssembly(OutputConfiguration outputConfigurations)
        {
            // register a custom assembly resolver.
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve +=(s, e) => ResolveAssembly(outputConfigurations, e.Name, e.RequestingAssembly.FullName);

            // loads the requested assembly, and extract the type.
            var assembly = Assembly.LoadFile(ResolvePath(outputConfigurations.Assembly));
            var type = assembly.ExportedTypes.FirstOrDefault(x => x.Name == outputConfigurations.EntryPointType);
            return type;
        }

        private static CodeGenOptions GetOptions(string[] args)
        {
            var options = new CodeGenOptions();

            if (!Parser.Default.ParseArguments(args, options))
                throw new Exception("Couldn't parse the command line arguments.");

            return options;
        }

        private static IEnumerable<string> GetFilesToProcess(CodeGenOptions options, string currentLocation)
        {
            var files = new List<string>();

            if (!string.IsNullOrWhiteSpace(options.FileName))
                files.Add(options.FileName);
            else if (options.Folder != null)
                files.AddRange(Directory.EnumerateFiles(Path.Combine(currentLocation, options.Folder), "*.json"));

            return files;
        }

        private static bool PrintHelp(CodeGenOptions options)
        {
            if (!options.Help)
                return false;

            LoggingService.Instance.Write(HelpText.AutoBuild(options).ToString());
            return true;
        }

        private static void PrintDefinitions(CodeGenOptions options)
        {
            if (!options.PrintObjectDefinition)
                return;

            LoggingService.Instance.WriteLine("Object Definitions:");

            foreach (var definition in Translator.Definitions.Definitions.Values)
            {
                LoggingService.Instance.WriteLine($"  - {definition.Name}");
            }
        }

        private static void PrintTranslations(CodeGenOptions options)
        {
            if (!options.PrintTranslations)
                return;

            LoggingService.Instance.WriteLine("Translators:");

            foreach (var translator in Translator.Translators.Translators.Values)
            {
                LoggingService.Instance.WriteLine($"  - {translator.Name} to {translator.Translation}");
            }
        }

        private static Assembly ResolveAssembly(OutputConfiguration configuration, string name, string requestedBy)
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
                LoggingService.Instance.WriteLine(assemblyName);
                LoggingService.Instance.Notice("Requested By:");
                LoggingService.Instance.WriteLine(requestedBy);

                return Assembly.LoadFile(assemblyName);
            }
            catch (Exception ex)
            {
                LoggingService.Instance.Error($"Problems loading assembly [{assemblyName ?? name}]: {ex.Message}");
                return null;
            }
        }

        private static string ResolvePath(string path)
        {
            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var fileName = Path.Combine(currentDirectory, path);
            return fileName;
        }

        private static string GetCurrentLocation()
        {
            var currentLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (currentLocation == null)
            {
                throw new Exception("Couldn't retrieve the current location.");
            }

            return currentLocation;
        }
    }
}
