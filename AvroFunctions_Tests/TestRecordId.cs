using AvroFunctions;
using NUnit.Framework;
using System.Collections.Generic;

namespace AvroFunctions_Tests
{
    public class TestRecordId
    {
        private AvroFunctions<RecordId> recordFuncs;

        [SetUp]
        public void Setup()
        {
            recordFuncs = new AvroFunctions<RecordId>();
        }

        public struct TestParams
        {
            public RecordId RecordId { get; set; }
            public string Serialized { get; set; }
        }

        public static IEnumerable<TestParams> GetRecords()
        {
            yield return new TestParams { RecordId = new RecordId(123, "123"), Serialized = "9gEGMTIz" };
            yield return new TestParams { RecordId = new RecordId(19191, "ThisIsAString"), Serialized = "7qsCGlRoaXNJc0FTdHJpbmc=" };
            yield return new TestParams { RecordId = new RecordId(890, "Testing_testing"), Serialized = "9A0eVGVzdGluZ190ZXN0aW5n" };
        }

        [Test]
        public void Serialization( [ValueSource(nameof(GetRecords))] TestParams Params )
        {
            string str = recordFuncs.Serialize(Params.RecordId);
            Assert.AreEqual(Params.Serialized, str);
        }

        [Test]
        public void Deserialization([ValueSource(nameof(GetRecords))] TestParams Params)
        {
            RecordId res = recordFuncs.Deserialize(Params.Serialized);
            Assert.AreEqual(Params.RecordId, res);
        }
    }
}