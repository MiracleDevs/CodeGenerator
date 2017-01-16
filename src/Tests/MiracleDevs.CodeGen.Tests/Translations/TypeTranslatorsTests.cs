using System;
using MiracleDevs.CodeGen.Logic.Translations;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class TypeTranslatorsTests
    {
        [Test]
        public void TypeTranslators()
        {
            var typeTranslator = new TypeTranslators();
            Assert.IsNotNull(typeTranslator.Translators);
        }

        [Test]
        public void OpenFailWithoutLocation()
        {
            Assert.Catch<Exception>(() => new TypeTranslators().Open("made-up-language"), "The translation file for language 'made-up-language' is missing.");
        }

        [Test]
        public void Open()
        {
            var typeTranslator = new TypeTranslators();
            
            typeTranslator.Open("typescript");

            Assert.IsNotNull(typeTranslator);
            Assert.IsNotNull(typeTranslator.Translators);
            Assert.AreEqual(typeTranslator.Translators.Count, 34);
        }

        [Test]
        public void Add()
        {
            var translator = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.Add(translator);

            Assert.AreEqual(typeTranslator.Translators.Count, 1);
            Assert.AreEqual(typeTranslator[typeof(int)], translator);
            Assert.AreEqual(typeTranslator.Get(typeof(int).ToString()), translator);
        }

        [Test]
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

        [Test]
        public void AddNull()
        {
            Assert.Catch<ArgumentNullException>(() => new TypeTranslators().Add(null), "Value can not be null.");
        }

        [Test]
        public void AddNullRange()
        {
            Assert.Catch<ArgumentNullException>(() => new TypeTranslators().AddRange(null), "Value can not be null.");            
        }

        [Test]
        public void GetWithNullName()
        {
            var translator = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.Add(translator);
            
            Assert.Catch<ArgumentNullException>(() => typeTranslator.Get(null as string), "Value can not be null.");
        }

        [Test]
        public void GetWithNullType()
        {
            var translator = new TypeTranslator { Name = "System.Int32", Translation = "number" };
            var typeTranslator = new TypeTranslators();

            typeTranslator.Add(translator);

            Assert.Catch<ArgumentNullException>(() => typeTranslator.Get(null as Type), "Value can not be null.");
        }
    }
}