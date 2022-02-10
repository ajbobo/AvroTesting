using AvroFunctions;
using NUnit.Framework;
using System.Collections.Generic;

namespace AvroFunctions_Tests
{
    public class TestDataRecord
    {
        private AvroFunctions<DataRecord> recordFuncs;

        [SetUp]
        public void Setup()
        {
            recordFuncs = new AvroFunctions<DataRecord>();
        }

        public struct TestParams
        {
            public DataRecord Record { get; set; }
            public string Serialized { get; set; }
        }

        public static IEnumerable<TestParams> GetParams()
        {
            /* These cases are from before the "Four" field was added */
            yield return new TestParams { 
                Record = new DataRecord(1, 2, new RecordId(3, "three"), "33"),
                Serialized = "AgQGCnRocmVlBDMz"
            };
            yield return new TestParams
            {
                Record = new DataRecord(12, 23, new RecordId(18, "stuff"), "HelloThere"),
                Serialized = "GC4kCnN0dWZmFEhlbGxvVGhlcmU="
            };
            yield return new TestParams
            {
                Record = new DataRecord(99, 22, new RecordId(3, "three"), ""),
                Serialized = "xgEsBgp0aHJlZQA="
            };
            yield return new TestParams
            {
                Record = new DataRecord(5, 10, new RecordId(4, "four4four"), "NumberThree"),
                Serialized = "ChQIEmZvdXI0Zm91chZOdW1iZXJUaHJlZQ=="
            };

            /* These cases include the "Four" field in the serialization */
        }

        [Test]
        public void Serialization( [ValueSource(nameof(GetParams))] TestParams Params )
        {
            string str = recordFuncs.Serialize(Params.Record);
            Assert.AreEqual(Params.Serialized, str);
        }

        [Test]
        public void Deserialization([ValueSource(nameof(GetParams))] TestParams Params)
        {
            DataRecord res = recordFuncs.Deserialize(Params.Serialized);
            Assert.AreEqual(Params.Record, res);
        }
    }
}