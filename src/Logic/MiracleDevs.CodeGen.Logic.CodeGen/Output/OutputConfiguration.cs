using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules;
using MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers;
using MiracleDevs.CodeGen.Logic.Logging;
using Newtonsoft.Json;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output
{
    [DataContract]
    public class OutputConfiguration
    {
        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string Assembly { get; set; }

        [DataMember]
        public string EntryPointType { get; set; }

        [DataMember]
        public List<OutputFileConfiguration> FileConfigurations { get; set; }

        public static OutputConfiguration Open(string fileName)
        {
            var cfg =  JsonConvert.DeserializeObject<OutputConfiguration>(File.ReadAllText(fileName));

            LoggingService.Instance.WriteLine($"Opening configuration for language: [{cfg.Language}]");
           
            foreach (var outputConfiguration in cfg.FileConfigurations)
            {
                if (outputConfiguration.NamingRules == null)
                {
                    outputConfiguration.NamingRules = new List<NamingRuleConfiguration>();
                    LoggingService.Instance.Warning($"The output configuration {outputConfiguration.CodeGenerationName} does not have naming rules.");
                }

                if (outputConfiguration.TypeMatchers == null)
                {
                    outputConfiguration.TypeMatchers = new List<TypeMatcherConfiguration>();
                    LoggingService.Instance.Warning($"The output configuration {outputConfiguration.CodeGenerationName} does not have type matchers.");
                }

                if (outputConfiguration.Parameters == null)
                {
                    outputConfiguration.Parameters = new List<ConfigurationParameter>();
                }
            }

            return cfg;
        }
    }
}