using System.Linq;
using MiracleDevs.CodeGen.Logic.Translations;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Translations
{
    [TestFixture]
    public class PropertyDefinitionTests
    {
        [Test]
        public void PropertyDefinition()
        {
            var propertyInfo = typeof(PropertyDefinitionTests).GetProperty(nameof(PublicProperty));
            var propertyDefinition = new PropertyDefinition(propertyInfo);

            Assert.IsNotNull(propertyDefinition);
            Assert.IsNotNull(propertyDefinition.Attributes);
            Assert.AreEqual(propertyDefinition.Name, nameof(PublicProperty));
            Assert.AreEqual(propertyDefinition.NativePropertyInfo, propertyInfo);

            Assert.AreEqual(propertyDefinition.Attributes.Count, 1);
            Assert.AreEqual(propertyDefinition.Attributes.First().NativeAttribute.GetType(), typeof(Mocks.PropertyAttribute));

            Assert.AreEqual(propertyDefinition.Type.NativeType, typeof(int));
        }

        [Mocks.Property]
        public int PublicProperty { get; set; }
    }
}