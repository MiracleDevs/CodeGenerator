using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiracleDevs.CodeGen.Logic.Translations;

namespace MiracleDevs.CodeGen.Tests
{
    [TestClass]
    public class TypeTranslatorsTests
    {
        [TestMethod]
        public void TypeTranslators()
        {
            var typeTranslator = new TypeTranslators();
            Assert.IsNotNull(typeTranslator.Translators);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "The translation file for language 'made-up-language' is missing.")]
        public void OpenFailWithoutLocation()
        {
            new TypeTranslators().Open("made-up-language");
        }

        [TestMethod]
        public void Open()
        {
            var typeTranslator = new TypeTranslators();
            
            typeTranslator.Open("typescript");

            Assert.IsNotNull(typeTranslator);
            Assert.IsNotNull(typeTranslator.Translators);
            Assert.AreEqual(typeTranslator.Translators.Count, 34);
        }

        [TestMethod]
        public void Add()
        {
            var translator = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.Add(translator);

            Assert.AreEqual(typeTranslator.Translators.Count, 1);
            Assert.AreEqual(typeTranslator[typeof(int)], translator);
            Assert.AreEqual(typeTranslator.Get(typeof(int).ToString()), translator);
        }

        [TestMethod]
        public void AddRange()
        {
            var translator1 = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var translator2 = new TypeTranslator { Name = "System.String", Translation = "string" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.AddRange(new []{ translator1, translator2 });

            Assert.AreEqual(typeTranslator.Translators.Count, 2);
            Assert.AreEqual(typeTranslator[typeof(int)], translator1);
            Assert.AreEqual(typeTranslator[typeof(string)], translator2);
            Assert.AreEqual(typeTranslator.Get(typeof(int).ToString()), translator1);
            Assert.AreEqual(typeTranslator.Get(typeof(string).ToString()), translator2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value can not be null.")]
        public void AddNull()
        {
            new TypeTranslators().Add(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value can not be null.")]
        public void AddNullRange()
        {
            new TypeTranslators().AddRange(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value can not be null.")]
        public void GetWithNullName()
        {
            var translator = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.Add(translator);

            typeTranslator.Get(null as string);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Value can not be null.")]
        public void GetWithNullType()
        {
            var translator = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.Add(translator);

            typeTranslator.Get(null as Type);
        }
    }
}