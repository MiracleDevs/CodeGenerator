using System.Linq;
using System.Reflection;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class PropertyDefinition: MemberDefinition
    {
        public PropertyInfo NativePropertyInfo { get; set; }

        public PropertyDefinition(PropertyInfo info)
        {
            this.Name = info.Name;
            this.Type = Translator.Definitions.GetDefinition(info.PropertyType);
            this.Attributes = info.GetCustomAttributes().Select(x => new AttributeDefinition(x)).ToList();
            this.NativePropertyInfo = info;
        }

        public override string ToString()
        {
            return $"property {this.Type.Name} {this.Name}";
        }
    }
}