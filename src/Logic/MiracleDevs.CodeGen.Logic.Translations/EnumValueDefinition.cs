namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class EnumValueDefinition
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public EnumValueDefinition(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}