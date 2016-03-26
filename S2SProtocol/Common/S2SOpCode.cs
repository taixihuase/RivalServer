//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：S2SOpCode.cs
//
// 文件功能描述：
//
// 子服务器向主服务器发送请求操作类别
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
    /// 名称：S2SOpCode
    /// 作者：taixihuase
    /// 作用：主从服务器间操作代码枚举值
    /// 编写日期：2016/1/25
    /// </summary>
    public enum S2SOpCode
    {
        #region Sub Server Op

        RegistSubServer,
        ReportServerLoad,

        #endregion

        #region Client Op To Login Server

        ClientApplyForCaptcha,
        ClientLogin,
        ClientRegist,
        ClientRegistCheck,

        #endregion

    }
}
