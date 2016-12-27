using System;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.TypeMatchers
{
    public class IncludesInNameTypeMatcher: TypeMatcherBase
    {
        protected override bool IsMatch(TypeMatcherConfiguration configuration, ObjectDefinition objectDefinition)
        {
            if (configuration.Parameters.Length != 1)
                throw new Exception("Includes In FileName type matcher has only 1 argument, the string to be found.");

            return objectDefinition.Name.Contains(configuration.Parameters[0]);
        }
    }
}