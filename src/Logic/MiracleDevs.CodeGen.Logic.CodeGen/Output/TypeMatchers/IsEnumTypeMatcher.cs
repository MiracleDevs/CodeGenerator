using System;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers
{
    public class IsEnumTypeMatcher : TypeMatcherBase
    {
        protected override bool IsMatch(TypeMatcherConfiguration configuration, ObjectDefinition objectDefinition)
        {
            if (configuration.Parameters.Length != 0)
                throw new Exception("Is Enum type matcher hasn't any arguments.");

            return objectDefinition.NativeType.IsEnum;
        }
    }
}