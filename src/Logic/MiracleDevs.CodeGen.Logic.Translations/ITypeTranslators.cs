using System;
using System.Collections.Generic;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public interface ITypeTranslators
    {
        void Add(TypeTranslator translator);
        void AddRange(IEnumerable<TypeTranslator> translators);
        TypeTranslator Get(Type type);
        TypeTranslator Get(string typeName);
    }
}