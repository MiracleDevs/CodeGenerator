using System.Collections.Generic;
using MiracleDevs.CodeGen.Logic.Translations;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class ObjectDefinitionFactoryTests
    {
        #region Nested Types

        private class TestClass
        {
        }

        private enum TestEnum
        {
        }

        private struct TestStruct
        {
        }

        #endregion

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