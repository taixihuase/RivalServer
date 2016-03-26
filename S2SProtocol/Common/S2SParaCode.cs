//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：S2SParaCode.cs
//
// 文件功能描述：
//
// 子服务器与主服务器间数据包传输的参数类别
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

namespace S2SProtocol.Common
{
    /// <summary>
    /// 类型：枚举
    /// 名称：S2SParaCode
    /// 作者：taixihuase
    /// 作用：主从服务器间数据参数代码枚举值
    /// 编写日期：2016/1/25
    /// </summary>
    public enum S2SParaCode
    {
        #region Para for Sub Server Op

        SubServerInfo,
        SubServerLoad,
        SubServerSocket,
        SocketList,

        #endregion

        #region Para for Client To Login Server Op

        ClientSocket,
        ClientEmail,
        ClientRegist,
        ClientLogin,

        #endregion
    }
}
