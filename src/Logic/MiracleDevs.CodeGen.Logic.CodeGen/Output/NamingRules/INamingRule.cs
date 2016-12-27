namespace MiracleDevs.CodeGen.Logic.CodeGen.Output.NamingRules
{
    public interface INamingRule
    {
        string Execute(string name, NamingRuleConfiguration configuration);
    }
}