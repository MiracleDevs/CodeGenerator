using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers
{
    public interface ITypeMatcher
    {
        bool Match(TypeMatcherConfiguration configuration, ObjectDefinition objectDefinition);
    }
}