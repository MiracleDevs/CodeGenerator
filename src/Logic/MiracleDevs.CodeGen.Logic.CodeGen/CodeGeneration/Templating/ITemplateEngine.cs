namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration.Templating
{
    public interface ITemplateEngine
    {
        string Execute(ITemplate template, object model);
    }
}