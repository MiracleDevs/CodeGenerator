using System;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class ObjectDefinitionsTests
    {       
        [Test]
        public void ObjectDefinitions()
        {
            var definitions = new ObjectDefinitions();
            Assert.IsNotNull(definitions.Definitions);
        }

        [Test]
        public void ProcessNullEntryPointType()
        {
            Assert.Catch<ArgumentNullException>(() => new ObjectDefinitions().ProcessEntryPointType(null), "Value can not be null.");
        }

        [Test]
        public void ProcessEntryPointType()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestClass));            
            Assert.Greater(Translator.Definitions.Definitions.Count, 0);
        }

        [Test]
        public void GetDefinition()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestClass));

            Assert.NotNull(Translator.Definitions.GetDefinition(typeof(TestClass)));
            Assert.NotNull(Translator.Definitions.GetDefinition(typeof(TestItem)));
        }

        [Test]
        public void Find()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestClass));
            Assert.NotNull(Translator.Definitions.Find(x => x.Name == nameof(TestClass)));
        }
    }
}