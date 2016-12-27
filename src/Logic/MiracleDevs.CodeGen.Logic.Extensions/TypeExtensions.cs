using System;
using System.Linq;

namespace MiracleDevs.CodeGen.Logic.Extensions
{
    public static class TypeExtensions
    {
        public static string GetRealGenericName(this Type t)
        {
            var name = t.Name;
            var index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }

        public static string GetReadableName(this Type type)
        {
            return type.IsGenericType ? $"{type.GetRealGenericName()}<{string.Join(", ", type.GenericTypeArguments.Select(x => x.GetReadableName()))}>" : type.Name;
        }

    }
}