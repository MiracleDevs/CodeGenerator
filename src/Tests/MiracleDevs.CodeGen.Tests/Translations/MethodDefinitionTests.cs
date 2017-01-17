using System.Linq;
using MiracleDevs.CodeGen.Logic.Translations;
using MiracleDevs.CodeGen.Tests.Translations.Mocks;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class MethodDefinitionTests
    {
        [Test]
        public void MethodDefinition()
        {
            var methodInfo = typeof(MethodDefinitionTests).GetMethod(nameof(PublicMethod));
            var methodDefinition = new MethodDefinition(methodInfo);

            Assert.IsNotNull(methodDefinition);
            Assert.IsNotNull(methodDefinition.Attributes);
            Assert.IsNotNull(methodDefinition.Parameters);
            Assert.IsNotNull(methodDefinition.ReturnType);

            Assert.AreEqual(methodDefinition.Name, nameof(PublicMethod));
            Assert.AreEqual(methodDefinition.NativeMethodInfo, methodInfo);
            Assert.AreEqual(methodDefinition.ReturnType.NativeType, typeof(int));

            Assert.AreEqual(methodDefinition.Attributes.Count, 1);
            Assert.AreEqual(methodDefinition.Attributes.First().NativeAttribute.GetType(), typeof(MethodAttribute));

            Assert.AreEqual(methodDefinition.Parameters.Count, 2);
            Assert.AreEqual(methodDefinition.Parameters[0].Name, "parameter1");
            Assert.AreEqual(methodDefinition.Parameters[0].Type.NativeType, typeof(string));

            Assert.AreEqual(methodDefinition.Parameters[1].Name, "parameter2");
            Assert.AreEqual(methodDefinition.Parameters[1].Type.NativeType, typeof(float));

            Assert.AreEqual(methodDefinition.ReturnType.NativeType, typeof(int));
        }
        
        [Method]
        public int PublicMethod(string parameter1, float parameter2)
        {
            return 0;
        }
    }
}