using Avro;
using Avro.Generic;
using Microsoft.Hadoop.Avro;
using System;
using System.IO;

namespace AvroFunctions
{
    public class WithCode<T>
    {
        private static readonly IAvroSerializer<T> serializer = AvroSerializer.Create<T>();

        public string Serialize(T obj)
        {
            string res;
            using (var stream = new MemoryStream())
            {
                try
                {
                    serializer.Serialize(stream, obj);
                    res = Convert.ToBase64String(stream.ToArray());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    res = "";
                }
            }
            return res;
        }

        public T Deserialize(string str)
        {
            T res;
            using (var stream = new MemoryStream(Convert.FromBase64String(str)))
            {
                res = serializer.Deserialize(stream);
            }
            return res;
        }
    }

    public class NoCode
    {
        private static string dataRecordJson = File.ReadAllText("Avro_Generated\\DataRecord.avsc");
        private static RecordSchema schema = (RecordSchema)Schema.Parse(dataRecordJson);

        public static string SerializeDataRecord(int v1, int v2, string v3, int id_num, string id_str)
        {
            // Create the DataRecord without using the generated C# code
            GenericRecord dataRecord = new GenericRecord(schema);
            dataRecord.Add("one", v1);
            dataRecord.Add("two", v2);
            dataRecord.Add("id", new GenericRecord((RecordSchema)schema["id"].Schema));
            (dataRecord["id"] as GenericRecord).Add("id_num", id_num);
            (dataRecord["id"] as GenericRecord).Add("id_str", id_str);
            dataRecord.Add("three", v3);

            string res;
            using (var stream = new MemoryStream())
            {
                DatumWriter<GenericRecord> writer = new GenericDatumWriter<GenericRecord>(schema);
                writer.Write(dataRecord, new Avro.IO.BinaryEncoder(stream));
                res = Convert.ToBase64String(stream.ToArray());
            }

            return res;
        }

        public static GenericRecord DeserializeDataRecord(string serialized)
        {
            GenericRecord res;
            using (var stream = new MemoryStream(Convert.FromBase64String(serialized)))
            {
                DatumReader<GenericRecord> reader = new GenericDatumReader<GenericRecord>(schema, schema);
                res = reader.Read(null, new Avro.IO.BinaryDecoder(stream));
            }
            return res;
        }
    }
}
