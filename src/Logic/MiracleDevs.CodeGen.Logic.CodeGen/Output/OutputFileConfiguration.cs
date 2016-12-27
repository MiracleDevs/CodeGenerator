using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules;
using MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output
{
    [DataContract]
    public class OutputFileConfiguration
    {
        [IgnoreDataMember]
        public string ExecutingFullPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
   
        [DataMember]
        public string OutputPath { get; set; }

        [DataMember]
        public string CodeGenerationName { get; set; }

        [DataMember]
        public List<ConfigurationParameter> Parameters { get; set; }

        [DataMember]
        public List<NamingRuleConfiguration> NamingRules { get; set; }

        [DataMember]
        public List<TypeMatcherConfiguration> TypeMatchers { get; set; }

        public string this[string name]
        {
            get
            {
                var entry = this.Parameters.FirstOrDefault(x => x.Name == name);

                if (entry == null)
                    throw new Exception($"Parameter {name} does not exist in output configuration.");

                return entry.Value;
            }
        }
    }
}