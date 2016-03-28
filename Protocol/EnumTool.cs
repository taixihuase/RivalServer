//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：EnumTool.cs
//
// 文件功能描述：
//
// 枚举工具类
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
using System.ComponentModel;
using System.Reflection;

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：EnumTool
    /// 作者：taixihuase
    /// 作用：枚举工具类
    /// 编写日期：2016/1/25
    /// </summary>
    public static class EnumTool
    {
        /// <summary>
        /// 类型：方法
        /// 名称：GetDescription
        /// 作者：taixihuase
        /// 作用：获取枚举描述信息
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumName)
        {
            string description;
            FieldInfo fieldInfo = enumName.GetType().GetField(enumName.ToString());
            DescriptionAttribute[] attributes = fieldInfo.GetDescriptAttr();
            if (attributes != null && attributes.Length > 0)
                description = attributes[0].Description;
            else
                throw new ArgumentException($"{enumName} 未能找到对应的枚举描述.", nameof(enumName));
            return description;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetDescriptAttr
        /// 作者：taixihuase
        /// 作用：获取枚举描述属性
        /// 编写日期：2016/3/28
        /// </summary>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        private static DescriptionAttribute[] GetDescriptAttr(this FieldInfo fieldInfo)
        {
            return (DescriptionAttribute[]) fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetEnum
        /// 作者：taixihuase
        /// 作用：通过描述获取枚举值
        /// 编写日期：2016/3/28
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static TEnum GetEnum<TEnum>(string description)
        {
            Type type = typeof(TEnum);
            foreach (FieldInfo field in type.GetFields())
            {
                DescriptionAttribute[] curDesc = field.GetDescriptAttr();
                if (curDesc != null && curDesc.Length > 0)
                {
                    if (curDesc[0].Description == description)
                        return (TEnum)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (TEnum)field.GetValue(null);
                }
            }
            throw new ArgumentException($"{description} 未能找到对应的枚举.", nameof(description));
        }
    }
}
