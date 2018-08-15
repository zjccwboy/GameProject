using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;

namespace TestBson
{
    public static class DataConvert
    {
        public static string ToJson(BsonDocument bson)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BsonBinaryWriter(stream))
                {
                    BsonSerializer.Serialize(writer, typeof(BsonDocument), bson);
                }
                stream.Seek(0, SeekOrigin.Begin);
                using (var reader = new Newtonsoft.Json.Bson.BsonReader(stream))
                {
                    var sb = new StringBuilder();
                    var sw = new StringWriter(sb);
                    using (var jWriter = new JsonTextWriter(sw))
                    {
                        jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                        jWriter.WriteToken(reader);
                    }
                    return sb.ToString();
                }
            }
        }

        public static string ToJson<T>(T data)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = new BsonBinaryWriter(stream))
                {
                    BsonSerializer.Serialize(writer, typeof(T), data);
                    return writer.ToString();
                }
                //stream.Seek(0, SeekOrigin.Begin);
                //using (var reader = new Newtonsoft.Json.Bson.BsonReader(stream))
                //{
                //    var sb = new StringBuilder();
                //    var sw = new StringWriter(sb);
                //    using (var jWriter = new JsonTextWriter(sw))
                //    {
                //        jWriter.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                //        jWriter.WriteToken(reader);
                //    }
                //    return sb.ToString();
                //}
            }
        }
    }
}
