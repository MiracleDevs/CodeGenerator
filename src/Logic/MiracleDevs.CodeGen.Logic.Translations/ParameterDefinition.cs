using System.Linq;
using System.Reflection;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class ParameterDefinition: MemberDefinition
    {
        public ParameterInfo NativeParameterInfo { get; set; }

        public ParameterDefinition(ParameterInfo info)
        {
            this.Name = info.Name;
            this.Type = Translator.Definitions.GetDefinition(info.ParameterType);
            this.Attributes = info.GetCustomAttributes().Select(x => new AttributeDefinition(x)).ToList();
            this.NativeParameterInfo = info;
        }

        public override string ToString()
        {
            return $"parameter {this.Type.Name} {this.Name}";
        }
    }
}