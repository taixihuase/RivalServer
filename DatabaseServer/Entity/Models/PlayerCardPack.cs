//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：PlayerCardPack.cs
//
// 文件功能描述：
//
// PlayerCardPack 实体
//
// 创建标识：taixihuase 20160414
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace DatabaseServer.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：PlayerCardPack
    /// 作者：taixihuase
    /// 作用：PlayerCardPack 实体
    /// 编写日期：2016/4/14
    /// </summary>
    public class PlayerCardPack
    {
        public int Id { get; set; }

        public virtual Player Player { get; set; }

        public int CardPackId { get; set; }

        public virtual CardPack CardPack { get; set; }

        public int Count { get; set; }
    }
}
