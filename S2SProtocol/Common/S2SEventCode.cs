//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：S2SEventCode.cs
//
// 文件功能描述：
//
// 主服务器向子服务器广播事件类别
//
// 创建标识：taixihuase 20160214
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

namespace S2SProtocol.Common
{
    /// <summary>
    /// 类型：枚举
    /// 名称：S2SEventCode
    /// 作者：taixihuase
    /// 作用：主从服务器间事件代码枚举值
    /// 编写日期：2016/2/14
    /// </summary>
    public enum S2SEventCode
    {
        ConnectLogicServer,
        DisconnectLogicServer,
        ConnectLobbyServer,
        DisconnectLobbyServer,
        ConnectProxyServer,
        DisconnectProxyServer,
        SetDefaultLogicServer,
    }
}
