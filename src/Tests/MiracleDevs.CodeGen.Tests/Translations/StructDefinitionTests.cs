using System.Linq;
using System.Runtime.Serialization;
using MiracleDevs.CodeGen.Logic.Extensions;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class StructDefinitionTests
    {
        [Test]
        public void StructDefinition()
        {
            var structDefition = new StructDefinition(typeof(TestStruct));
            Assert.IsNotNull(structDefition);
            Assert.IsNotNull(structDefition.Methods);
            Assert.IsNotNull(structDefition.Properties);
            Assert.IsNotNull(structDefition.Attributes);

            Assert.AreEqual(structDefition.Name, typeof(TestStruct).GetReadableName());
            Assert.AreEqual(structDefition.NativeType, typeof(TestStruct));
            Assert.AreEqual(structDefition.Namespace, typeof(TestStruct).Namespace);
        }

        [Test]
        public void Process()
        {
            var structDefition = new StructDefinition(typeof(TestStruct));

            structDefition.Process();
         
            Assert.AreEqual(structDefition.Attributes.Count, 1);
            Assert.AreEqual(structDefition.Attributes.First().NativeAttribute.GetType(), typeof(DataContractAttribute));

            Assert.AreEqual(structDefition.Properties.Count, 1);
            Assert.AreEqual(structDefition.Properties.First().Name, nameof(TestStruct.Items));

            Assert.AreEqual(structDefition.Methods.Count, 1);
            Assert.AreEqual(structDefition.Methods.First().Name, nameof(TestStruct.PublicMethod));

            Assert.IsNull(structDefition.InnerObject);

            Assert.IsNull(structDefition.Translator);
            Assert.IsFalse(structDefition.IsArray);
            Assert.IsFalse(structDefition.HasTranslation);
        }
    }
}