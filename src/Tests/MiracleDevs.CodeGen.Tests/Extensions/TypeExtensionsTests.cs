using System.Collections.Generic;
using MiracleDevs.CodeGen.Logic.Extensions;
using NUnit.Framework;

namespace MiracleDevs.CodeGen.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void GetBasicTypeRealGenericNameTest()
        {
            var type = typeof(int);
            Assert.AreEqual(type.GetRealGenericName(), "Int32");
        }

        [Test]
        public void GetBasicTypeReadableNameTest()
        {
            var type = typeof(int);
            Assert.AreEqual(type.GetReadableName(), "Int32");
        }

        [Test]
        public void GetGenericTypeRealGenericNameTest()
        {
            var type = typeof(List<int>);
            Assert.AreEqual(type.GetRealGenericName(), "List");
        }

        [Test]
        public void GetGenericTypeReadableNameTest()
        {
            var type = typeof(List<int>);
            Assert.AreEqual(type.GetReadableName(), "List<Int32>");
        }
    }
}