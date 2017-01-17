using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MiracleDevs.CodeGen.Tests.Translations.Mocks
{
    [DataContract]
    public struct TestStruct
    {
        [DataMember]
        public List<TestItem> Items { get; set; }

        internal List<string> InternalItems { get; set; }

        private List<string> PrivateItems { get; set; }

        public static List<TestItem> PublicStaticItems { get; set; }

        private static List<string> PrivateStaticItems { get; set; }

        public void PublicMethod() { }

        private void PrivateMethod() { }

        public static void PublicStaticMethod() { }

        private static void PrivateStaticMethod() { }
    }
}