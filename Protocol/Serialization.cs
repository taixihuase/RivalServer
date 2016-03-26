//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：Serialization.cs
//
// 文件功能描述：
//
// 数据对象的序列化及反序列化
//
// 创建标识：taixihuase 20160125
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using ProtoBuf;

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：Example
    /// 作者：taixihuase
    /// 作用：进行序列化和反序列化的类的示例
    /// 编写日期：2016/1/25
    /// </summary>
    [ProtoContract]
    public class Example
    {
        // Protobuf 要求
        // 不带 [ProtoMember] C#特性的成员其值将不被序列化
        // IsRequired 是可选的
        // 如成员是自定义类对象，即该类是嵌套类，序号必须与类成员对象中的第一个成员序号相同
        // 若将所有自定义类成员在该类中展开，不能存在相同的序号

        public int ClassName { get; set; }

        [ProtoMember(1, IsRequired = true)]
        public Dictionary<int, int> Dictionary { get; set; }

        // 必须带有无参数默认构造函数
        public Example()
        {
            Dictionary = new Dictionary<int, int>();
        }
    }

    /// <summary>
    /// 类型：类
    /// 名称：Serialization
    /// 作者：taixihuase
    /// 作用：对数据进行二进制序列化与反序列化
    /// 编写日期：2016/1/25
    /// </summary>
    public static class Serialization
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Serialize
        /// 作者：taixihuase
        /// 作用：将一个对象二进制序列化
        /// 编写日期：2016/1/25
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static byte[] Serialize<T>(T instance)
        {
            byte[] bytes;
            using (var ms = new MemoryStream())
            {
                Serializer.Serialize(ms, instance);
                bytes = new byte[ms.Position];
                var fullBytes = ms.GetBuffer();
                Array.Copy(fullBytes, bytes, bytes.Length);
            }
            return bytes;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Deserialize
        /// 作者：taixihuase
        /// 作用：将一个二进制序列化数据流反序列化为一个对象实例
        /// 编写日期：2016/1/25
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Deserialize<T>(object obj)
        {
            byte[] bytes = (byte[]) obj;
            using (var ms = new MemoryStream(bytes))
            {
                return Serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsNeed
        /// 作者：taixihuase
        /// 作用：判断对象是否需要被序列化
        /// 编写日期：2016/3/26
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNeed(object obj)
        {
            if (obj is byte[] || obj is string || obj is int[] || obj is byte || obj is bool || obj is short || obj is int || obj is long || obj is float || obj is double)
                return false;
            return true;
        }

    //    /// <summary>
    //    /// 类型：方法
    //    /// 名称：ToJson
    //    /// 作者：taixihuase
    //    /// 作用：将一个对象序列化成Json字符串
    //    /// 编写日期：2016/1/25
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    /// <returns></returns>
    //    public static string ToJson(object obj)
    //    {
    //        return JsonConvert.SerializeObject(obj);
    //    }

    //    /// <summary>
    //    /// 类型：方法
    //    /// 名称：FromJson
    //    /// 作者：taixihuase
    //    /// 作用：将一个Json字符串反序列化为对象实例
    //    /// 编写日期：2016/1/25
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <param name="str"></param>
    //    /// <returns></returns>
    //    public static T FromJson<T>(string str)
    //    {
    //        return JsonConvert.DeserializeObject<T>(str);
    //    }
    }
}
