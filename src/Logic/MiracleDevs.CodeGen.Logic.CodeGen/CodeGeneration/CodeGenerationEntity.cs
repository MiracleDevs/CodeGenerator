using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration
{
    public class CodeGenerationEntity
    {
        public string Type { get; set; }

        public ObjectDefinition Definition { get; set; }

        public CodeGenerationEntity(string type, ObjectDefinition definition)
        {
            Type = type;
            Definition = definition;
        }
    }
}