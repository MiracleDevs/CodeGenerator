using System.Linq;
using System.Runtime.Serialization;
using MiracleDevs.CodeGen.Logic.Extensions;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class ClassDefinitionTests
    {
        [Test]
        public void ClassDefinition()
        {
            var classDefition = new ClassDefinition(typeof(TestClass));
            Assert.IsNotNull(classDefition);
            Assert.IsNotNull(classDefition.Methods);
            Assert.IsNotNull(classDefition.Properties);
            Assert.IsNotNull(classDefition.Attributes);

            Assert.AreEqual(classDefition.Name, typeof(TestClass).GetReadableName());
            Assert.AreEqual(classDefition.NativeType, typeof(TestClass));
            Assert.AreEqual(classDefition.Namespace, typeof(TestClass).Namespace);
        }

        [Test]
        public void Process()
        {
            var classDefition = new ClassDefinition(typeof(TestClass));

            classDefition.Process();

            Assert.AreEqual(classDefition.Attributes.Count, 1);
            Assert.AreEqual(classDefition.Attributes.First().NativeAttribute.GetType(), typeof(DataContractAttribute));

            Assert.AreEqual(classDefition.Properties.Count, 1);
            Assert.AreEqual(classDefition.Properties.First().Name, nameof(TestClass.Items));

            Assert.AreEqual(classDefition.Methods.Count, 1);
            Assert.AreEqual(classDefition.Methods.First().Name, nameof(TestClass.PublicMethod));

            Assert.IsNull(classDefition.InnerObject);

            Assert.IsNull(classDefition.Translator);
            Assert.IsFalse(classDefition.IsArray);
            Assert.IsFalse(classDefition.HasTranslation);
        }
    }
}