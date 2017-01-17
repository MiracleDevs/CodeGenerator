using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public class TypeTranslators : ITypeTranslators
    {
        #region Properties

        public Dictionary<string, TypeTranslator> Translators { get; set; }

        public TypeTranslator this[Type type] => this.Get(type);

        #endregion

        #region Constructor

        public TypeTranslators()
        {
            this.Translators = new Dictionary<string, TypeTranslator>();
        }

        #endregion

        #region Public Methods

        public void Open(string language)
        {
            this.Translators.Clear();

            var location = Assembly.GetExecutingAssembly().Location;

            if (location == null)
                throw new Exception("Assembly Location couldn't be retrieved.");

            var file = Path.Combine(Path.GetDirectoryName(location), Configuration.ConfigurationFolder, language, Configuration.TypeTranslationsFile);

            if (!File.Exists(file))
                throw new Exception($"The translation file for language '{language}' is missing.");

            this.AddRange(JsonConvert.DeserializeObject<TypeTranslator[]>(File.ReadAllText(file)));
        }

        public void Add(TypeTranslator translator)
        {
            if (translator == null)
                throw new ArgumentNullException(nameof(translator));

            this.Translators.Add(translator.Name, translator);
        }

        public void AddRange(IEnumerable<TypeTranslator> translators)
        {
            if (translators == null)
                throw new ArgumentNullException(nameof(translators));

            foreach (var translator in translators)
            {
                this.Add(translator);
            }
        }

        public TypeTranslator Get(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return this.Get(type.Name) ?? this.Get(type.ToString());
        }

        public TypeTranslator Get(string typeName)
        {
            if (typeName == null)
                throw new ArgumentNullException(nameof(typeName));

            return !this.Translators.ContainsKey(typeName) ? null : this.Translators[typeName];
        }

        #endregion
    }
}