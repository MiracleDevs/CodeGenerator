using System;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers
{
    public class InheritsFromTypeMatcher : TypeMatcherBase
    {
        protected override bool IsMatch(TypeMatcherConfiguration configuration, ObjectDefinition objectDefinition)
        {
            if (configuration.Parameters.Length != 1)
                throw new Exception("Inherits From type matcher has only 1 argument, the string to be found.");

            var typeName = configuration.Parameters[0];
            var type = Type.GetType(typeName);

            if (type == null)
                throw new Exception($"Type '{typeName}' couldn't be found.");

            return type.IsAssignableFrom(objectDefinition.NativeType);
        }
    }
}