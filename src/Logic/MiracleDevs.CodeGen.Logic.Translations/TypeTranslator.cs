using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    [DataContract]
    public class TypeTranslator
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Translation { get; set; }

        public override string ToString()
        {
            return $"{this.Name}: {this.Translation}";
        }
    }
}