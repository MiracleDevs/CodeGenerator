using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MiracleDevs.CodeGen.Logic.Extensions;

namespace MiracleDevs.CodeGen.Logic.Translations
{
    public abstract class ObjectDefinition
    {
        public Type NativeType { get; set; }

        public string Name { get; set; }

        public string Namespace { get; set; }

        public TypeTranslator Translator { get; set; }

        public List<AttributeDefinition> Attributes { get; set; }

        public bool HasTranslation => this.Translator != null;

        public ObjectDefinition InnerObject { get; set; }

        public bool IsArray { get; set; }

        protected ObjectDefinition()
        {
            this.Attributes = new List<AttributeDefinition>();
        }

        protected ObjectDefinition(Type type)
        {        
            this.NativeType = type;
            this.Name = type.GetReadableName();
            this.Namespace = type.Namespace;
            this.Attributes = new List<AttributeDefinition>();
        }

        public virtual void Process()
        {
            if (this.HasTranslation)
                return;

            this.Attributes = this.NativeType.GetCustomAttributes().Select(x => new AttributeDefinition(x)).ToList();

            if (this.NativeType.IsArray && this.NativeType != typeof(string))
            {
                this.IsArray = true;
                this.InnerObject = Translations.Translator.Definitions.GetDefinition(this.NativeType.GetElementType() ?? typeof(object));
            }
            else if (typeof(IEnumerable).IsAssignableFrom(this.NativeType) && this.NativeType != typeof(string))
            {
                this.IsArray = true;
                this.InnerObject = Translations.Translator.Definitions.GetDefinition((this.NativeType.IsGenericType
                    ? this.NativeType.GenericTypeArguments.FirstOrDefault() 
                    : this.NativeType.GetElementType()) ?? typeof(object));
            }

            this.Translator = Translations.Translator.Translators.Get(this.NativeType);
        }
    }
}