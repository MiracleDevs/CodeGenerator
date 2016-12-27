using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules
{
    [DataContract]
    public class NamingRuleConfiguration
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string[] Parameters { get; set; }
    }
}