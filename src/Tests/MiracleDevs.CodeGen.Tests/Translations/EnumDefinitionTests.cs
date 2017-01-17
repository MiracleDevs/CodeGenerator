using System.Linq;
using System.Runtime.Serialization;
using MiracleDevs.CodeGen.Logic.Extensions;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class EnumDefinitionTests
    {
        [Test]
        public void EnumDefinition()
        {
            var enumDefition = new EnumDefinition(typeof(TestEnum));

            Assert.IsNotNull(enumDefition);
            Assert.IsNotNull(enumDefition.Attributes);

            Assert.AreEqual(enumDefition.Name, typeof(TestEnum).GetReadableName());
            Assert.AreEqual(enumDefition.NativeType, typeof(TestEnum));
            Assert.AreEqual(enumDefition.Namespace, typeof(TestEnum).Namespace);
        }

        [Test]
        public void Process()
        {
            var enumDefition = new EnumDefinition(typeof(TestEnum));

            enumDefition.Process();

            Assert.AreEqual(enumDefition.Attributes.Count, 1);
            Assert.AreEqual(enumDefition.Attributes.First().NativeAttribute.GetType(), typeof(DataContractAttribute));

            Assert.AreEqual(enumDefition.Values.Count, 3);
            Assert.AreEqual(enumDefition.Values[0].Name, nameof(TestEnum.Value1));
            Assert.AreEqual(enumDefition.Values[0].Value, ((int)TestEnum.Value1).ToString());
            Assert.AreEqual(enumDefition.Values[1].Name, nameof(TestEnum.Value2));
            Assert.AreEqual(enumDefition.Values[1].Value, ((int)TestEnum.Value2).ToString());
            Assert.AreEqual(enumDefition.Values[2].Name, nameof(TestEnum.Value3));
            Assert.AreEqual(enumDefition.Values[2].Value, ((int)TestEnum.Value3).ToString());
   
            Assert.IsNull(enumDefition.InnerObject);
            Assert.IsNull(enumDefition.Translator);
            Assert.IsFalse(enumDefition.IsArray);
            Assert.IsFalse(enumDefition.HasTranslation);
        }
    }
}