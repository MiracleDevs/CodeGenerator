using System;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public static class ObjectDefinitionFactory
    {
        public static ObjectDefinition Create(Type type)
        {
            if (type.IsEnum)
                return new EnumDefinition(type);

            if (type.IsValueType)
                return new StructDefinition(type);

            return new ClassDefinition(type);
        }
    }
}