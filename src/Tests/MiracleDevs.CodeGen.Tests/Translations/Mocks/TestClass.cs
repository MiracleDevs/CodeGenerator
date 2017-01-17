using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Tests.Translations.Mocks
{
    [DataContract]
    public class TestClass
    {
        [DataMember]
        public List<TestItem> Items { get; set; }

        internal List<TestItem> InternalItems { get; set; }

        protected List<string> ProtectedItems { get; set; }

        private List<string> PrivateItems { get; set; }

        public static List<TestItem> PublicStaticItems { get; set; }

        protected static List<string> ProtectedStaticItems { get; set; }

        private static List<string> PrivateStaticItems { get; set; }

        public TestClass()
        {

        }

        public void PublicMethod() { }

        internal void InternalMethod() { }

        protected void ProtectedMethod() { }

        private void PrivateMethod() { }

        public static void PublicStaticMethod() { }

        protected static void ProtectedStaticMethod() { }

        private static void PrivateStaticMethod() { }
    }
}