using Avro.Generic;
using AvroFunctions;
using NUnit.Framework;
using System.Collections.Generic;

namespace AvroFunctions_Tests
{
    public class TestDataRecord
    {
        private WithCode<DataRecord> recordFuncs;

        [SetUp]
        public void Setup()
        {
            recordFuncs = new WithCode<DataRecord>();
        }

        public struct TestParams
        {
            public DataRecord Record { get; set; }
            public string Serialized { get; set; }
            public object[] RawVals { get; set; }
        }

        public static IEnumerable<TestParams> GetParams()
        {
            /* These cases are from before the "Four" field was added */
            yield return new TestParams { 
                Record = new DataRecord(1, 2, new RecordId(3, "three"), "33"),
                RawVals = new object[] { 1, 2, 3, "three", "33" },
                Serialized = "AgQGCnRocmVlBDMz"
            };
            yield return new TestParams
            {
                Record = new DataRecord(12, 23, new RecordId(18, "stuff"), "HelloThere"),
                RawVals = new object[] { 12, 23, 18, "stuff", "HelloThere" },
                Serialized = "GC4kCnN0dWZmFEhlbGxvVGhlcmU="
            };
            yield return new TestParams
            {
                Record = new DataRecord(99, 22, new RecordId(3, "three"), ""),
                RawVals = new object[] { 99, 22, 3, "three", "" },
                Serialized = "xgEsBgp0aHJlZQA="
            };
            yield return new TestParams
            {
                Record = new DataRecord(5, 10, new RecordId(4, "four4four"), "NumberThree"),
                RawVals = new object[] { 5, 10, 4, "four4four", "NumberThree" },
                Serialized = "ChQIEmZvdXI0Zm91chZOdW1iZXJUaHJlZQ=="
            };

            /* These cases include the "Four" field in the serialization */
        }

        [Test]
        public void Serialization([ValueSource(nameof(GetParams))] TestParams Params)
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

        [Test]
        public void SerializeWithoutCode([ValueSource(nameof(GetParams))] TestParams Params)
        {
            object[] vals = Params.RawVals;
            string str = NoCode.SerializeDataRecord((int)vals[0], (int)vals[1], (string)vals[4], (int)vals[2], (string)vals[3]);
            Assert.AreEqual(Params.Serialized, str);
        }

        [Test]
        public void DeserializeWithoutCode([ValueSource(nameof(GetParams))] TestParams Params)
        {
            GenericRecord record = NoCode.DeserializeDataRecord(Params.Serialized);

            Assert.AreEqual(Params.Record.one, record["one"]);
            Assert.AreEqual(Params.Record.two, record["two"]);
            Assert.AreEqual(Params.Record.three, record["three"]);
            Assert.AreEqual(Params.Record.id.id_num, (record["id"] as GenericRecord)["id_num"]);
            Assert.AreEqual(Params.Record.id.id_str, (record["id"] as GenericRecord)["id_str"]);
        }
    }
}