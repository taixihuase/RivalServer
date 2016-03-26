//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ServerType.cs
//
// 文件功能描述：
//
// 子服务器类型枚举
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

using System.ComponentModel;

namespace S2SProtocol.Common
{
    /// <summary>
    /// 类型：枚举
    /// 名称：ServerType
    /// 作者：taixihuase
    /// 作用：子服务器类型
    /// 编写日期：2016/1/25
    /// </summary>
    public enum ServerType
    {
        [Description("UndefinedServer")] UndefinedServer,

        [Description("MasterServer")] MasterServer,

        [Description("DatabaseServer")] DatabaseServer,

        [Description("LoginServer")] LoginServer,

        [Description("ProxyServer")] ProxyServer,

        [Description("LobbyServer")] LobbyServer,

        [Description("LogicServer")] LogicServer,
    }
}
