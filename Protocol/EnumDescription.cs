//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：EnumDescription.cs
//
// 文件功能描述：
//
// 枚举描述工具类
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

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：EnumDescription
    /// 作者：taixihuase
    /// 作用：枚举描述工具类
    /// 编写日期：2016/1/25
    /// </summary>
    public static class EnumDescription
    {
        /// <summary>
        /// 类型：方法
        /// 名称：GetEnumDescription
        /// 作者：taixihuase
        /// 作用：获取枚举描述信息
        /// 编写日期：2016/1/25
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription<TEnum>(object value)
        {
            Type enumType = typeof (TEnum);
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("enumItem requires a Enum ");
            }
            var name = Enum.GetName(enumType, Convert.ToInt32(value));
            if (name == null)
                return string.Empty;
            object[] objs = enumType.GetField(name).GetCustomAttributes(typeof (DescriptionAttribute), false);
            return objs.Length == 0 ? string.Empty : ((DescriptionAttribute) objs[0]).Description;
        }
    }
}
