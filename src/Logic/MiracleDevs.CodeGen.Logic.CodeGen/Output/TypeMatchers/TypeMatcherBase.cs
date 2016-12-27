using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers
{
    public abstract class TypeMatcherBase : ITypeMatcher
    {
        public bool Match(TypeMatcherConfiguration configuration, ObjectDefinition objectDefinition)
        {
            if (configuration.Negate)
                return !this.IsMatch(configuration, objectDefinition);

            return this.IsMatch(configuration, objectDefinition);
        }

        protected abstract bool IsMatch(TypeMatcherConfiguration configuration, ObjectDefinition objectDefinition);

    }
}