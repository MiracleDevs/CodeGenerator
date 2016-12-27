using System.Collections.Generic;
using MiracleDevs.CodeGen.Logic.CodeGen.Output;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration.Templating
{
    public class TemplateModel
    {
        public OutputFileConfiguration Configuration { get; set; }

        public ObjectDefinition Definition { get; set; }

        public IEnumerable<ObjectDefinition> Definitions { get; set; }

        public TemplateModel(OutputFileConfiguration configuration, ObjectDefinition definition, IEnumerable<ObjectDefinition> definitions)
        {
            Configuration = configuration;
            Definition = definition;
            Definitions = definitions;
        }
    }
}