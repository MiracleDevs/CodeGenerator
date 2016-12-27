using System;

namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules
{
    public class FormatNamingRule : INamingRule
    {
        public string Execute(string name, NamingRuleConfiguration configuration)
        {
            if (configuration.Parameters.Length != 1)
                throw new Exception("Format rule has only 1 argument, the format string.");

            return string.Format(configuration.Parameters[0], name);
        }
    }
}