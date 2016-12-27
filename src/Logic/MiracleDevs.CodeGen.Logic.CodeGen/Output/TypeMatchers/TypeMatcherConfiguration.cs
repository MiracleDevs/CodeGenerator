using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers
{
    [DataContract]
    public class TypeMatcherConfiguration
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string[] Parameters { get; set; }

        [DataMember]
        public bool Negate { get; set; }
    }
}
