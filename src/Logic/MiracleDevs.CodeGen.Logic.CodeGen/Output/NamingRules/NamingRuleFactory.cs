using System;
using System.Reflection;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules
{
    public static class NamingRuleFactory
    {
        public static INamingRule Create(string name)
        {
            var typeName = $"{typeof(NamingRuleFactory).Namespace}.{name}NamingRule";
            var type = Assembly.GetExecutingAssembly().GetType(typeName);

            if (type == null)
                throw new Exception($"The naming rule {name} does not exist.");

            var instance = Activator.CreateInstance(type) as INamingRule;

            if (instance == null)
                throw new Exception($"The naming rule {name} couldn't be instantiated.");

            return instance;
        }
    }
}