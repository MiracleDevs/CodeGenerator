using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration.Templating;
using MiracleDevs.CodeGen.Logic.CodeGen.Output;
using MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules;
using MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers;
using MiracleDevs.CodeGen.Logic.Logging;
using MiracleDevs.CodeGen.Logic.Translations;
using Newtonsoft.Json;

namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration
{
    public class CodeGenerator
    {
        private List<CodeGenerationConfiguration> CodeGenerationConfigurations { get; }

        private ITemplateEngine TemplateEngine { get; }

        public string Language { get; }

        public CodeGenerator(string language)
        {
            this.Language = language;
            var location = Assembly.GetExecutingAssembly().Location;

            if (location == null)
                throw new Exception("Assembly Location couldn't be retrieved.");

            var file = Path.Combine(Path.GetDirectoryName(location), $"{Configuration.ConfigurationFolder}/{language}/{Configuration.CodeGenerationConfiguration}");

            if (!File.Exists(file))
                throw new Exception($"The codegeneration configuration file for language '{language}' is missing.");

            this.CodeGenerationConfigurations = JsonConvert.DeserializeObject<List<CodeGenerationConfiguration>>(File.ReadAllText(file));
            this.TemplateEngine = new RazorTemplateEngine();
        }

        public void Generate(OutputConfiguration outputConfigurations)
        {
            LoggingService.Instance.WriteLine("Begining Code Generation...");

            foreach (var outputConfiguration in outputConfigurations.FileConfigurations)
            {
                ProcessCodeGeneration(outputConfiguration);
            }
        }

        private void ProcessCodeGeneration(OutputFileConfiguration outputConfiguration)
        {
            try
            {
                var codegenConfiguration = this.CodeGenerationConfigurations.FirstOrDefault(x => x.Name == outputConfiguration.CodeGenerationName);

                if (codegenConfiguration == null)
                    throw new Exception($"The code generation configuration for ${outputConfiguration.CodeGenerationName} does not exist.");

                LoggingService.Instance.WriteLine($" - Code generation configuration found for: {outputConfiguration.CodeGenerationName}");

                foreach (var objectDefiniton in Translator.Definitions.Definitions.Values)
                {
                    if (ProcessObjectDefinition(objectDefiniton, outputConfiguration, codegenConfiguration))
                    {
                        LoggingService.Instance.WriteLine("");
                    }
                }
            }
            catch (Exception e)
            {
                LoggingService.Instance.Error($"Problems found in output configuration '{outputConfiguration.CodeGenerationName}':{Environment.NewLine}{e.Message}");
            }
        }

        private bool ProcessObjectDefinition(ObjectDefinition objectDefiniton, OutputFileConfiguration outputConfiguration, CodeGenerationConfiguration codegenConfiguration)
        {
            try
            {
                if (!this.IsMatch(objectDefiniton, outputConfiguration))
                    return false;

                LoggingService.Instance.WriteLine($"     - Object definition '{objectDefiniton.Name}' matches the configuration.");
                this.GenerateFile(outputConfiguration, codegenConfiguration, objectDefiniton);
                return true;            
            }
            catch (Exception e)
            {
                LoggingService.Instance.Error($"Problems found in object definition '{objectDefiniton.Name}':{Environment.NewLine}{e.Message}");
                return false;
            }
        }

        private void GenerateFile(OutputFileConfiguration outputConfiguration, CodeGenerationConfiguration codegenConfiguration, ObjectDefinition definition)
        {
            var outputFilename = this.ResolveOutputName(outputConfiguration, definition);                     
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
            var outputDirectory = Path.Combine(directory, outputConfiguration.OutputPath);
            var outputPath = Path.Combine(outputDirectory, outputFilename);

            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            LoggingService.Instance.WriteLine($"     - Choosen FileName:  {outputFilename}");

            var templateFile = Path.Combine(directory, codegenConfiguration.TemplateFile);
            var template = new RazorTemplate(templateFile);
            File.WriteAllText(outputPath, this.TemplateEngine.Execute(template, new TemplateModel(outputConfiguration, definition, Translator.Definitions.Definitions.Values)));
        }

        private string ResolveOutputName(OutputFileConfiguration outputConfiguration, ObjectDefinition definition)
        {
            return outputConfiguration.NamingRules.Aggregate(definition.Name, (current, rule) => NamingRuleFactory.Create(rule.Name).Execute(current, rule));
        }

        private bool IsMatch(ObjectDefinition objectDefinition, OutputFileConfiguration outputConfiguration)
        {
            return outputConfiguration.TypeMatchers.Any() &&
                   outputConfiguration.TypeMatchers.Aggregate(true, (current, typeMatcherConfiguration) => current & TypeMatcherFactory.Create(typeMatcherConfiguration.Name).Match(typeMatcherConfiguration, objectDefinition));
        }
    }
}