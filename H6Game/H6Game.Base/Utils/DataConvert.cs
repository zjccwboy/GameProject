using Newtonsoft.Json;
using System;
using System.Text;
using ProtoBuf;
using System.IO;

namespace H6Game.Base
{
    /// <summary>
    /// Json系列化辅助类
    /// </summary>
    public static class DataConvert
    {
        private static Type stringType = typeof(string);
        private readonly static JsonSerializerSettings settings = new JsonSerializerSettings();
        static DataConvert()
        {
            settings.NullValueHandling = NullValueHandling.Ignore;
        }

        /// <summary>
        /// object转json string
        /// </summary>
        /// <typeparam name="T">泛型对象,class约束</typeparam>
        /// <param name="obj">泛型对象,class约束</param>
        /// <returns>json string</returns>
        public static string ToJson<T>(this T obj) where T : class, new()
        {
            if (obj == null)
                return null;

            var json = JsonConvert.SerializeObject(obj, typeof(T), settings);
            return json;
        }

        /// <summary>
        /// object转json string
        /// </summary>
        /// <typeparam name="T">泛型对象,class约束</typeparam>
        /// <param name="obj">泛型对象,class约束</param>
        /// <param name="formatting">json格式化参数</param>
        /// <returns>json string</returns>
        public static string ToJson<T>(this T obj, Formatting formatting) where T : class, new()
        {
            if(obj == null)
                return null;

            var json = JsonConvert.SerializeObject(obj,typeof(T), formatting, settings);
            return json;
        }

        /// <summary>
        /// json string转object
        /// </summary>
        /// <typeparam name="T">泛型对象,class约束</typeparam>
        /// <param name="json">json string</param>
        /// <returns>泛型对象,class约束</returns>
        public static T JsonToObject<T>(this string json) where T : class, new()
        {
            if (string.IsNullOrEmpty(json))
                return null;

            var obj = JsonConvert.DeserializeObject<T>(json, settings);
            return obj;
        }

        /// <summary>
        /// object转bytes
        /// </summary>
        /// <typeparam name="T">泛型对象,class约束</typeparam>
        /// <param name="obj">泛型对象,class约束</param>
        /// <param name="formatting">json格式化参数</param>
        /// <returns>json bytes</returns>
        public static byte[] ToBytes<T>(this T obj) where T : class, new()
        {
            if (obj == null)
                return null;

            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize(ms, obj);
                ms.Position = 0;
                var bytes = new byte[ms.Length];
                ms.Read(bytes, 0, (int)ms.Length);
                return bytes;
            }
        }

        /// <summary>
        /// json bytes转object
        /// </summary>
        /// <param name="bytes">json bytes</param>
        /// <param name="type">type</param>
        /// <returns>泛型对象,class约束</returns>
        public static T ProtoToObject<T>(this byte[] bytes)
        {
            if (bytes == null)
                return default;

            var type = typeof(T);
            if (TryGetBaseTypeValue(bytes, type, out object obj))
                return (T)obj;

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return Serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// json bytes转object
        /// </summary>
        /// <param name="bytes">json bytes</param>
        /// <param name="type">type</param>
        /// <returns>泛型对象,class约束</returns>
        public static object ProtoToObject(this byte[] bytes, Type type)
        {
            if (bytes == null)
                return null;

            if(TryGetBaseTypeValue(bytes, type, out object obj))
                return obj;

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                obj = Serializer.Deserialize(type, ms);
                return obj;
            }
        }


        public static bool TryGetBaseTypeValue(this byte[] bytes, Type type, out object value)
        {
            if(type == typeof(int))
            {
                value = BitConverter.ToInt32(bytes, 0);
                return true;
            }
            else if(type == typeof(int))
            {
                value = BitConverter.ToInt32(bytes, 0);
                return true;
            }
            else if(type == typeof(bool))
            {
                value = BitConverter.ToBoolean(bytes, 0);
                return true;
            }
            else if(type == typeof(float))
            {
                value = BitConverter.ToSingle(bytes, 0);
                return true;
            }
            else if(type == typeof(decimal))
            {
                value = BitConverter.ToDouble(bytes, 0);
                return true;
            }
            else if(type == typeof(string))
            {
                value = Encoding.UTF8.GetString(bytes);
                return true;
            }
            else if(type == typeof(double))
            {
                value = BitConverter.ToDouble(bytes, 0);
                return true;
            }
            else if(type == typeof(short))
            {
                value = BitConverter.ToInt16(bytes, 0);
                return true;
            }
            else if(type == typeof(ushort))
            {
                value = BitConverter.ToUInt16(bytes, 0);
                return true;
            }
            else if(type == typeof(byte))
            {
                value = bytes[0];
                return true;
            }
            else if(type == typeof(sbyte))
            {
                value = bytes[0];
                return true;
            }
            else if(type == typeof(char))
            {
                value = BitConverter.ToChar(bytes, 0);
                return true;
            }
            else if(type == typeof(long))
            {
                value = BitConverter.ToInt64(bytes, 0);
                return true;
            }
            else if(type == typeof(ulong))
            {
                value = BitConverter.ToUInt64(bytes, 0);
                return true;
            }
            value = null;
            return false;
        }
    }
}
