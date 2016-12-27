using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output
{
    [DataContract]
    public class ConfigurationParameter
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Value { get; set; }
    }
}