using System;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public static class Translator
    {
        private static readonly Lazy<TypeTranslators> TypeTranslators = new Lazy<TypeTranslators>(() => new TypeTranslators(), true);

        private static readonly Lazy<ObjectDefinitions> ObjectDefinitions = new Lazy<ObjectDefinitions>(() => new ObjectDefinitions(), true);

        public static TypeTranslators Translators => TypeTranslators.Value;

        public static ObjectDefinitions Definitions => ObjectDefinitions.Value;
    }
}