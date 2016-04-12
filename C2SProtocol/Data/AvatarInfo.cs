//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：AvatarInfo.cs
//
// 文件功能描述：
//
// 记录头像基本信息
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
//----------------------------------------------------------------------------------------------------------

using ProtoBuf;

namespace C2SProtocol.Data
{
    /// <summary>
    /// 类型：类
    /// 名称：AvatarInfo
    /// 作者：taixihuase
    /// 作用：记录头像基本信息
    /// 编写日期：2016/4/12
    /// </summary>
    [ProtoContract]
    public class AvatarInfo
    {
        /// <summary>
        /// 类型：方法
        /// 名称：AvatarInfo
        /// 作者：taixihuase
        /// 作用：默认构造头像数据
        /// 编写日期：2016/4/12
        /// </summary>
        public AvatarInfo()
        {
            AvatarId = 0;
            Name = string.Empty;
        }

        [ProtoMember(100)]
        public int AvatarId { get; set; }

        [ProtoMember(101)]
        public string Name { get; set; }
    }
}
