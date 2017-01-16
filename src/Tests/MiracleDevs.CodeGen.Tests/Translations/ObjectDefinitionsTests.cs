using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MiracleDevs.CodeGen.Logic.Translations;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class ObjectDefinitionsTests
    {
        #region Nested Types

        [DataContract]
        public class TestItem
        {
            [DataMember]
            public int Integer { get; set; }

            [DataMember]
            public string String { get; set; }

            [DataMember]
            public bool Boolean { get; set; }

            [DataMember]
            public DateTime DateTime { get; set; }

            [DataMember]
            public List<int> List { get; set; }
        }

        [DataContract]
        public class TestParent
        {
            [DataMember]
            public List<TestItem> Items { get; set; }

            protected List<string> ProtectedItems { get; set; }

            private List<string> PrivateItems { get; set; }

            public static List<TestItem> PublicStaticItems { get; set; }

            protected List<string> ProtectedStaticItems { get; set; }

            private List<string> PrivateStaticItems { get; set; }

            public TestParent()
            {
                
            }

            public void PublicMethod() { }

            protected void ProtectedMethod() { }

            private void PrivateMethod() { }

            public static void PublicStaticMethod() { }

            protected static void ProtectedStaticMethod() { }

            private static void PrivateStaticMethod() { }
        }

        #endregion

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
            Translator.Definitions.ProcessEntryPointType(typeof(TestParent));            
            Assert.Greater(Translator.Definitions.Definitions.Count, 0);
        }

        [Test]
        public void GetDefinition()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestParent));

            Assert.NotNull(Translator.Definitions.GetDefinition(typeof(TestParent)));
            Assert.NotNull(Translator.Definitions.GetDefinition(typeof(TestItem)));
        }

        [Test]
        public void Find()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestParent));
            Assert.NotNull(Translator.Definitions.Find(x => x.Name == "TestParent"));
        }

        [Test]
        public void ReadPublicNonStaticDeclaredProperties()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestParent));
            var objectDefinition = Translator.Definitions.Find(x => x.Name == "TestParent").FirstOrDefault() as ClassDefinition;
            Assert.IsInstanceOf<ClassDefinition>(objectDefinition);

            Assert.AreEqual(objectDefinition.Properties.Count, 1);
            Assert.AreEqual(objectDefinition.Properties.First().Name, "Items");
        }

        [Test]
        public void ReadPublicNonStaticDeclaredMethods()
        {
            Translator.Definitions.ProcessEntryPointType(typeof(TestParent));
            var objectDefinition = Translator.Definitions.Find(x => x.Name == "TestParent").FirstOrDefault() as ClassDefinition;
            Assert.IsInstanceOf<ClassDefinition>(objectDefinition);

            Assert.AreEqual(objectDefinition.Methods.Count, 1);
            Assert.AreEqual(objectDefinition.Methods.First().Name, "PublicMethod");
        }
    }
}