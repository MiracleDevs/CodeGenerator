using System;
using System.IO;
using System.Text;

namespace MiracleDevs.CodeGen.Logic.CodeGen.CodeGeneration.Templating
{
    public class RazorTemplate : ITemplate
    {
        public string Body { get; private set; }

        public string FileName { get; }

        public RazorTemplate(string fileName)
        {
            FileName = fileName;
            this.Body = File.ReadAllText(fileName);

            this.ProcessBody();
        }

        public void ProcessBody()
        {
            if (this.Body == null)
                return;

            int indexOfInclude;
            var directory = Path.GetDirectoryName(this.FileName);

            if (directory == null)
                return;

            while ((indexOfInclude = this.Body.IndexOf("@include", StringComparison.InvariantCulture)) >= 0)
            {
                var firstQuote = this.Body.IndexOf("\"", indexOfInclude, StringComparison.InvariantCulture);

                if (firstQuote < 0 || firstQuote >= this.Body.Length)
                    throw new Exception("Missing opening quote on include sentence.");

                var lastQuote = this.Body.IndexOf("\"", firstQuote + 1, StringComparison.InvariantCulture);

                if (lastQuote < 0)
                    throw new Exception("Missing ending quote on include sentence.");

                var path = Path.Combine(directory, this.Body.Substring(firstQuote + 1, lastQuote - firstQuote - 1));

                var builder = new StringBuilder(this.Body);

                builder.Remove(indexOfInclude, lastQuote - indexOfInclude);
                builder.Insert(indexOfInclude, File.ReadAllText(path));

                this.Body = builder.ToString();
            }
        }
    }
}