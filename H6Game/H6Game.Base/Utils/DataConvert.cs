using Newtonsoft.Json;
using System;

/// <summary>
/// Json系列化辅助类
/// </summary>
public static class DataConvert
{
    private static readonly Type SType = typeof(string);
    private readonly static JsonSerializerSettings JsonSettings = new JsonSerializerSettings();
    static DataConvert()
    {
        JsonSettings.NullValueHandling = NullValueHandling.Ignore;
    }

    /// <summary>
    /// object转json string
    /// </summary>
    /// <typeparam name="T">泛型对象,class约束</typeparam>
    /// <param name="obj">泛型对象,class约束</param>
    /// <returns>json string</returns>
    public static string ToJson<T>(this T obj) where T : class
    {
        if (obj == null)
            return null;

        var json = JsonConvert.SerializeObject(obj, typeof(T), JsonSettings);
        return json;
    }

    /// <summary>
    /// object转json string
    /// </summary>
    /// <typeparam name="T">泛型对象,class约束</typeparam>
    /// <param name="obj">泛型对象,class约束</param>
    /// <param name="formatting">json格式化参数</param>
    /// <returns>json string</returns>
    public static string ToJson<T>(this T obj, Formatting formatting) where T : class
    {
        if (obj == null)
            return null;

        var json = JsonConvert.SerializeObject(obj, typeof(T), formatting, JsonSettings);
        return json;
    }

    /// <summary>
    /// json string转object
    /// </summary>
    /// <typeparam name="T">泛型对象,class约束</typeparam>
    /// <param name="json">json string</param>
    /// <returns>泛型对象,class约束</returns>
    public static T JsonToObject<T>(this string json) where T : class
    {
        if (string.IsNullOrEmpty(json))
            return null;

        var obj = JsonConvert.DeserializeObject<T>(json, JsonSettings);
        return obj;
    }
}