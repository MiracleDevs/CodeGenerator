using System.Collections.Generic;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class ObjectDefinitionFactoryTests
    {
        [Test]
        public void CreateFromClass()
        {
            Assert.IsInstanceOf<ClassDefinition>(ObjectDefinitionFactory.Create(typeof(TestClass)));
        }

        [Test]
        public void CreateFromEnum()
        {
            Assert.IsInstanceOf<EnumDefinition>(ObjectDefinitionFactory.Create(typeof(TestEnum)));
        }

        [Test]
        public void CreateFromStruct()
        {
            Assert.IsInstanceOf<StructDefinition>(ObjectDefinitionFactory.Create(typeof(TestStruct)));
        }

        [Test]
        public void CreateFromArray()
        {
            Assert.IsInstanceOf<ClassDefinition>(ObjectDefinitionFactory.Create(typeof(TestClass[])));
        }

        [Test]
        public void CreateFromList()
        {
            Assert.IsInstanceOf<ClassDefinition>(ObjectDefinitionFactory.Create(typeof(List<TestEnum>)));
        }

        [Test]
        public void CreateFromEnumerable()
        {
            Assert.IsInstanceOf<ClassDefinition>(ObjectDefinitionFactory.Create(typeof(IEnumerable<TestStruct>)));
        }
    }
}