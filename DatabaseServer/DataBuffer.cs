//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：DataBuffer.cs
//
// 文件功能描述：
//
// 数据库数据缓存池
//
// 创建标识：taixihuase 20160412
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using C2SProtocol.Data;

namespace DatabaseServer
{
    /// <summary>
    /// 类型：类 
    /// 名称：DataBuffer
    /// 作者：taixihuase
    /// 作用：数据缓存池
    /// 编写日期：2016/4/12
    /// </summary>
    public class DataBuffer
    {
        public static readonly DataBuffer Instance = new DataBuffer();

        public Dictionary<int, UserInfo> User = new Dictionary<int, UserInfo>(); 

        public Dictionary<int, PlayerInfo> Player = new Dictionary<int, PlayerInfo>(); 

        public Dictionary<int, AvatarInfo> Avatar = new Dictionary<int, AvatarInfo>();
        
        public Dictionary<int, TitleInfo> Title = new Dictionary<int, TitleInfo>(); 

        public Dictionary<int, CardInfo> Card = new Dictionary<int, CardInfo>();

        public Dictionary<int, CardEffectInfo> CardEffect = new Dictionary<int, CardEffectInfo>();
         
        public Dictionary<int, CardPackInfo> CardPack = new Dictionary<int, CardPackInfo>();

        /// <summary>
        /// 类型：方法
        /// 名称：DataBuffer
        /// 作者：taixihuase
        /// 作用：静态构造数据缓存池
        /// 编写日期：2016/4/12
        /// </summary>
        static DataBuffer()
        {
            
        }
    }
}
