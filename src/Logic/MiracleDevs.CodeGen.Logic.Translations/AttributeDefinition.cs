using System;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class AttributeDefinition
    {        
        public Attribute NativeAttribute { get; set; }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public AttributeDefinition(Attribute attribute)
        {
            this.NativeAttribute = attribute;
            this.Name = attribute.GetType().Name;
            this.Namespace = attribute.GetType().Namespace;
        }

        public override string ToString()
        {
            return $"attribute {this.Name}";
        }
    }
}