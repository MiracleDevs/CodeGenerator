using System;
using System.Collections.Generic;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public interface IObjectDefinitions
    {
        void ProcessEntryPointType(Type type);

        ObjectDefinition GetDefinition(Type type);

        IEnumerable<ObjectDefinition> Find(Func<ObjectDefinition, bool> predicate);
    }
}