using System.IO;
using System.Text;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration.Templating
{
    public class RazorTemplateEngine : ITemplateEngine
    {
        private IRazorEngineService EngineService { get; }

        public RazorTemplateEngine()
        {
            var config = new TemplateServiceConfiguration { /*CachingProvider = new DefaultCachingProvider(t => { })*/ };
            this.EngineService = RazorEngineService.Create(config);
        }

        public string Execute(ITemplate template, object model)
        {
            var builder = new StringBuilder();
            var key = this.EngineService.GetKey(template.FileName) ?? new NameOnlyTemplateKey(template.FileName, ResolveType.Global, null);
            this.EngineService.AddTemplate(key, new LoadedTemplateSource(template.Body));

            using (var sw = new StringWriter(builder))
            {
                this.EngineService.RunCompile(key, sw, model: model);
                return builder.ToString();
            }
        }
    }
}