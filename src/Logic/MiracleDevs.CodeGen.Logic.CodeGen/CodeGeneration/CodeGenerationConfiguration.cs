using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration
{
    [DataContract]
    public class CodeGenerationConfiguration
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TemplateFile { get; set; }
    }
}