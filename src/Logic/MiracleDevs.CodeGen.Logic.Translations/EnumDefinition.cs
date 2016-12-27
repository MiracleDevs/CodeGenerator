using System;
using System.Collections.Generic;
using System.Reflection;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class EnumDefinition : ObjectDefinition
    {      
        public List<EnumValueDefinition> Values { get; set; }

        public EnumDefinition()
        {
            this.Values = new List<EnumValueDefinition>();
        }

        public EnumDefinition(Type type) : base(type)
        {
            this.Values = new List<EnumValueDefinition>();
        }

        public override void Process()
        {
            if (this.HasTranslation)
                return;

            base.Process();

            var fields = this.NativeType.GetFields();

            foreach(var field in fields)
            {
                if (field.Name.Equals("value__"))
                    continue;

                this.Values.Add(new EnumValueDefinition(field.Name, field.GetRawConstantValue().ToString()));
            }
        }

        public override string ToString()
        {
            return $"enum {this.Name}";
        }
    }
}