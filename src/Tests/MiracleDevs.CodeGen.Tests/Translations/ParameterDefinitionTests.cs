using System.Linq;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class ParameterDefinitionTests
    {
        [Test]
        public void ParameterDefinition()
        {
            var parameterInfo = typeof(ParameterDefinitionTests).GetMethod(nameof(PublicMethod)).GetParameters().First();
            var parameterDefinition = new ParameterDefinition(parameterInfo);

            Assert.IsNotNull(parameterDefinition);
            Assert.IsNotNull(parameterDefinition.Attributes);
            Assert.AreEqual(parameterDefinition.Name, "parameter");
            Assert.AreEqual(parameterDefinition.NativeParameterInfo, parameterInfo);  

            Assert.AreEqual(parameterDefinition.Attributes.Count, 1);
            Assert.AreEqual(parameterDefinition.Attributes.First().NativeAttribute.GetType(), typeof(ParameterAttribute));

            Assert.AreEqual(parameterDefinition.Type.NativeType, typeof(string));
        }

        public void PublicMethod([Parameter]string parameter)
        {
        }
    }
}