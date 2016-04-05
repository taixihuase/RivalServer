//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：CombatAttribute.cs
//
// 文件功能描述：
//
// CombatAttribute 复杂类型
//
// 创建标识：taixihuase 20160404
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

namespace C2SProtocol.Entity.Models
{
    /// <summary>
    /// 类型：类
    /// 名称：CombatAttribute
    /// 作者：taixihuase
    /// 作用：CombatAttribute 复杂类型
    /// 编写日期：2016/4/4
    /// </summary>
    public class CombatAttribute
    {
        public string AttackAttribute { get; set; }

        public int Attack { get; set; }

        public string ShieldAttribute { get; set; }

        public int Shield { get; set; }
    }
}
