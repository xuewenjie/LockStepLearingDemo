using LitJson;
using System;

public class JsonSerializerUtil
{
    /// <summary> 把对象转换为Json字符串 </summary>
    /// <param name="obj">对象</param>
    public static string ToJson<T>(T obj)
    {
        return JsonMapper.ToJson(obj);
    }

    public static byte[] ToJsonByte<T>(T obj)
    {
        string json = ToJson<T>(obj);
        return System.Text.Encoding.Default.GetBytes(json);
    }

    /// <summary> 解析Json </summary>
    /// <typeparam name="T">类型</typeparam>
    /// <param name="json">Json字符串</param>
    public static T FromJson<T>(string json)
    {
        return JsonMapper.ToObject<T>(json);
    }

    public static T FromJsonByte<T>(byte[] bytes)
    {
        string json = System.Text.Encoding.Default.GetString(bytes);
        return FromJson<T>(json);
    }

    public static object FromJson(string json,Type type)
    {
        return JsonMapper.ToObject(json,type);
    }

    public static object FromJsonByte(byte[] bytes,Type type)
    {
        string json = System.Text.Encoding.Default.GetString(bytes);
        return FromJson(json, type);
    }

    

}
