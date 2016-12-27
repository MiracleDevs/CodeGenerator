using System.Collections.Generic;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public abstract class MemberDefinition
    {
        public string Name { get; set; }

        public ObjectDefinition Type { get; set; }

        public List<AttributeDefinition> Attributes { get; set; }

        protected MemberDefinition()
        {
            this.Attributes = new List<AttributeDefinition>();
        }
    }
}