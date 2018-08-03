using Newtonsoft.Json;
using System;
using System.Text;
using ProtoBuf;
using System.IO;
using System.Collections.Generic;

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
            if (TryGetValueType(bytes, type, out object obj))
            {
                var objVal = obj as ValueObject<T>;
                return objVal.Value;
            }

            using (var ms = new MemoryStream())
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return Serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// 无GC装箱转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetValueType(this byte[] bytes, Type type, out object value)
        {
            if(type == typeof(string))
            {
                value = Encoding.UTF8.GetString(bytes);
                return true;
            }

            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Int32:
                    {
                        var data = BitConverter.ToInt32(bytes, 0);
                        value = ValueObject<int>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.UInt32:
                    {
                        var data = BitConverter.ToUInt32(bytes, 0);
                        value = ValueObject<uint>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.Boolean:
                    {
                        var data = BitConverter.ToBoolean(bytes, 0);
                        value = ValueObject<bool>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.Int64:                    
                    {
                        var data = BitConverter.ToInt64(bytes, 0);
                        value = ValueObject<long>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.UInt64:
                    {
                        var data = BitConverter.ToUInt64(bytes, 0);
                        value = ValueObject<ulong>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.Single:
                    {
                        var data = BitConverter.ToSingle(bytes, 0);
                        value = ValueObject<float>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.Double:
                case TypeCode.Decimal:
                    {
                        var data = BitConverter.ToDouble(bytes, 0);
                        value = ValueObject<double>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.Byte:
                case TypeCode.SByte:
                    value = bytes[0];
                    return true;
                case TypeCode.Char:
                    {
                        var data = BitConverter.ToChar(bytes, 0);
                        value = ValueObject<char>.Instance.GetValue(data);
                        return true;
                    }    
                case TypeCode.Int16:
                    {
                        var data = BitConverter.ToInt16(bytes, 0);
                        value = ValueObject<short>.Instance.GetValue(data);
                        return true;
                    }
                case TypeCode.UInt16:
                    {
                        var data = BitConverter.ToUInt16(bytes, 0);
                        value = ValueObject<ushort>.Instance.GetValue(data);
                        return true;
                    }
            }
            value = default;
            return false;
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
            TypeCode code = Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Int32:
                    value = BitConverter.ToInt32(bytes, 0);
                    return true;
                case TypeCode.UInt32:
                    value = BitConverter.ToInt32(bytes, 0);
                    return true;
                case TypeCode.Boolean:
                    value = BitConverter.ToBoolean(bytes, 0);
                    return true;
                case TypeCode.Int64:
                    value = BitConverter.ToInt64(bytes, 0);
                    return true;
                case TypeCode.UInt64:
                    value = BitConverter.ToUInt64(bytes, 0);
                    return true;
                case TypeCode.Single:
                    value = BitConverter.ToSingle(bytes, 0);
                    return true;
                case TypeCode.Double:
                    value = BitConverter.ToDouble(bytes, 0);
                    return true;
                case TypeCode.Decimal:
                    value = BitConverter.ToDouble(bytes, 0);
                    return true;
                case TypeCode.Byte:
                    value = bytes[0];
                    return true;
                case TypeCode.SByte:
                    value = bytes[0];
                    return true;
                case TypeCode.Char:
                    value = BitConverter.ToChar(bytes, 0);
                    return true;
                case TypeCode.Int16:
                    value = BitConverter.ToInt16(bytes, 0);
                    return true;
                case TypeCode.UInt16:
                    value = BitConverter.ToUInt16(bytes, 0);
                    return true;
            }            
            value = null;
            return false;
        }
    }
}
