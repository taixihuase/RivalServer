﻿//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：EnumComparer.cs
//
// 文件功能描述：
//
// 泛型枚举比较器，用于进行字典中以枚举表示的键的比较
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
using System.Linq.Expressions;
using ProtoBuf;

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：EnumComparer
    /// 作者：taixihuase
    /// 作用：泛型枚举比较器类
    /// 编写日期：2016/1/25
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [ProtoContract]
    public class EnumComparer<T> : IEqualityComparer<T> where T : struct
    {
        #region IEqualityComparer<T> 泛型接口实现

        public bool Equals(T first, T second)
        {
            var firstParam = Expression.Parameter(typeof(T), "first");
            var secondParam = Expression.Parameter(typeof(T), "second");
            var equalExpression = Expression.Equal(firstParam, secondParam);

            return Expression.Lambda<Func<T, T, bool>>
                (equalExpression, firstParam, secondParam).
                Compile().Invoke(first, second);
        }

        public int GetHashCode(T instance)
        {
            var parameter = Expression.Parameter(typeof(T), "instance");
            var convertExpression = Expression.Convert(parameter, typeof(int));

            return Expression.Lambda<Func<T, int>>
                (convertExpression, parameter).
                Compile().Invoke(instance);
        }

        #endregion
    }
}
