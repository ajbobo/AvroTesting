//<auto-generated />
namespace AvroFunctions
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Microsoft.Hadoop.Avro;

    /// <summary>
    /// Used to serialize and deserialize Avro record AvroFunctions.RecordId.
    /// </summary>
    [DataContract(Namespace = "AvroFunctions")]
    public partial class RecordId
    {
        private const string JsonSchema = @"{""type"":""record"",""name"":""AvroFunctions.RecordId"",""fields"":[{""name"":""id_num"",""type"":""int""},{""name"":""id_str"",""type"":""string""}]}";

        /// <summary>
        /// Gets the schema.
        /// </summary>
        public static string Schema
        {
            get
            {
                return JsonSchema;
            }
        }
      
        /// <summary>
        /// Gets or sets the id_num field.
        /// </summary>
        [DataMember]
        public int id_num { get; set; }
              
        /// <summary>
        /// Gets or sets the id_str field.
        /// </summary>
        [DataMember]
        public string id_str { get; set; }
                
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordId"/> class.
        /// </summary>
        public RecordId()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordId"/> class.
        /// </summary>
        /// <param name="id_num">The id_num.</param>
        /// <param name="id_str">The id_str.</param>
        public RecordId(int id_num, string id_str)
        {
            this.id_num = id_num;
            this.id_str = id_str;
        }
    }
}
