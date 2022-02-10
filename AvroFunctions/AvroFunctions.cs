using Microsoft.Hadoop.Avro;
using System;
using System.IO;

namespace AvroFunctions
{
    public class AvroFunctions<T>
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
}
