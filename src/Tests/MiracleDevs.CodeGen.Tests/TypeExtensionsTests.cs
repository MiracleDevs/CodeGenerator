using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiracleDevs.CodeGen.Logic.Extensions;

namespace MiracleDevs.CodeGen.Tests
{
    [TestClass]
    public class TypeExtensionsTests
    {
        [TestMethod]
        public void GetBasicTypeRealGenericNameTest()
        {
            var type = typeof(int);
            Assert.AreEqual(type.GetRealGenericName(), "Int32");
        }

        [TestMethod]
        public void GetBasicTypeReadableNameTest()
        {
            var type = typeof(int);
            Assert.AreEqual(type.GetReadableName(), "Int32");
        }

        [TestMethod]
        public void GetGenericTypeRealGenericNameTest()
        {
            var type = typeof(List<int>);
            Assert.AreEqual(type.GetRealGenericName(), "List");
        }

        [TestMethod]
        public void GetGenericTypeReadableNameTest()
        {
            var type = typeof(List<int>);
            Assert.AreEqual(type.GetReadableName(), "List<Int32>");
        }
    }
}