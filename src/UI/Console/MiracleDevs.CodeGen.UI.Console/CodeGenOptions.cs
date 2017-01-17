using CommandLine;

namespace MiracleDevs.CodeGen.UI.Console
{
    public class CodeGenOptions
    {
        [Option('f', "filename", HelpText = "Indicates the path of a output configuration json file.")]
        public string FileName { get; set; }

        [Option('d', "directory", HelpText = "Indicates a directory path in which all the output configuration files will be used to generate code.")]
        public string Folder { get; set; }

        [Option('t', "print-trans", HelpText = "Prints all the translations.")]
        public bool PrintTranslations { get; set; }

        [Option('o', "print-objdef", HelpText = "Prints all the object definitions.")]
        public bool PrintObjectDefinition { get; set; }

        [Option('h', "help", HelpText = "Prompts the help.")]
        public bool Help { get; set; }
    }
}