using System;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class ClassDefinition : StructDefinition
    {
        public ClassDefinition(Type type): base(type)
        {
        }

        public override string ToString()
        {
            return $"class {this.Name}";
        }
    }
}