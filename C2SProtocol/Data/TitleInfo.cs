//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：TitleInfo.cs
//
// 文件功能描述：
//
// 记录头衔基本信息
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
    [ProtoContract]
    public class TitleInfo
    {
        public TitleInfo()
        {
            TitleId = 0;
            Name = string.Empty;
        }

        /// <summary>
        /// 编号
        /// </summary>
        [ProtoMember(1)]
        public int TitleId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ProtoMember(2)]
        public string Name { get; set; }
    }
}
