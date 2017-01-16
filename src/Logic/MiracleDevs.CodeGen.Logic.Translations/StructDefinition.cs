using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class StructDefinition : ObjectDefinition
    {
        public List<PropertyDefinition> Properties { get; set; }

        public List<MethodDefinition> Methods { get; set; }

        public StructDefinition()
        {
            this.Properties = new List<PropertyDefinition>();
            this.Methods = new List<MethodDefinition>();
        }

        public StructDefinition(Type type): base(type)
        {
            this.Properties = new List<PropertyDefinition>();
            this.Methods = new List<MethodDefinition>();
        }

        public override void Process()
        {
            if (this.HasTranslation)
                return;

            base.Process();
            this.Properties =  this.NativeType.GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Select(x => new PropertyDefinition(x)).ToList();
            this.Methods = this.NativeType.GetTypeInfo().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly).Where(x => !x.IsSpecialName).Select(x => new MethodDefinition(x)).ToList();
        }

        public override string ToString()
        {
            return $"struct {this.Name}";
        }
    }
}