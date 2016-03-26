//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ServerInfo.cs
//
// 文件功能描述：
//
// 记录服务器信息
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

using ProtoBuf;
using Protocol;
// ReSharper disable PossibleNullReferenceException
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace S2SProtocol.Common
{
    /// <summary>
    /// 类型：类
    /// 名称：ServerInfo
    /// 作者：taixihuase
    /// 作用：子服务器信息类
    /// 编写日期：2016/1/25
    /// </summary>
    [ProtoContract]
    public class ServerInfo
    {
        [ProtoMember(1)]
        // SocketGuid 类起始序号为 1，结束序号为 2
        public SocketGuid Socket { get; set; }

        [ProtoMember(3)]
        public ServerType ServerType { get; set; }

        [ProtoMember(4)]
        public ushort ListeningPort { get; set; }

        [ProtoMember(5)]
        public string ServerName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(ServerInfo))
                return false;

            ServerInfo key = obj as ServerInfo;
            return key.ServerName == ServerName && key.ServerType == ServerType && key.Socket == Socket && key.ListeningPort == ListeningPort;
        }

        public override int GetHashCode()
        {
            return Socket.IpAddress.GetHashCode();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetServerAddress
        /// 作者：taixihuase
        /// 作用：获取子服务器套接字地址字符串
        /// 编写日期：2016/1/25
        /// </summary>
        /// <returns></returns>
        public string GetServerAddress()
        {
            return Socket.GetSocketToString();
        }
    }
}
