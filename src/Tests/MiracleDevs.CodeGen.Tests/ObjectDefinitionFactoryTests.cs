using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Tests
{
    [TestClass]
    public class ObjectDefinitionFactoryTests
    {
        private class TestClass
        {            
        }

        private enum TestEnum
        {            
        }

        private struct TestStruct
        {           
        }

        [TestMethod]
        public void CreateFromClass()
        {
            Assert.IsInstanceOfType(ObjectDefinitionFactory.Create(typeof(TestClass)), typeof(ClassDefinition));
        }

        [TestMethod]
        public void CreateFromEnum()
        {
            Assert.IsInstanceOfType(ObjectDefinitionFactory.Create(typeof(TestEnum)), typeof(EnumDefinition));
        }

        [TestMethod]
        public void CreateFromStruct()
        {
            Assert.IsInstanceOfType(ObjectDefinitionFactory.Create(typeof(TestStruct)), typeof(StructDefinition));
        }

        [TestMethod]
        public void CreateFromArray()
        {
            Assert.IsInstanceOfType(ObjectDefinitionFactory.Create(typeof(TestClass[])), typeof(ClassDefinition));
        }

        [TestMethod]
        public void CreateFromList()
        {
            Assert.IsInstanceOfType(ObjectDefinitionFactory.Create(typeof(List<TestEnum>)), typeof(ClassDefinition));
        }

        [TestMethod]
        public void CreateFromEnumerable()
        {
            Assert.IsInstanceOfType(ObjectDefinitionFactory.Create(typeof(IEnumerable<TestStruct>)), typeof(ClassDefinition));
        }
    }
}