using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class MethodDefinition
    {
        public MethodInfo NativeMethodInfo { get; set; }

        public string Name { get; set; }

        public ObjectDefinition ReturnType { get; set; }

        public List<ParameterDefinition> Parameters { get; set; }

        public List<AttributeDefinition> Attributes { get; set; }

        public MethodDefinition(MethodInfo info)
        {
            this.Name = info.Name;
            this.ReturnType = Translator.Definitions.GetDefinition(info.ReturnType);
            this.Parameters = info.GetParameters().Select(x => new ParameterDefinition(x)).ToList();
            this.Attributes = info.GetCustomAttributes().Select(x => new AttributeDefinition(x)).ToList();
            this.NativeMethodInfo = info;
        }

        public override string ToString()
        {
            return $"method {this.ReturnType.Name} {this.Name}()";
        }
    }
}