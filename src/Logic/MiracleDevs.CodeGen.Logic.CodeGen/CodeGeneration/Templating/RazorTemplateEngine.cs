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
            // if template cache is required: { CachingProvider = new DefaultCachingProvider(t => { }) }
            var config = new TemplateServiceConfiguration();
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