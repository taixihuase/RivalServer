//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：SocketGuid.cs
//
// 文件功能描述：
//
// 基于套接字的全局唯一标识符工具类
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

using System.Net;
using ProtoBuf;

// ReSharper disable PossibleNullReferenceException
// ReSharper disable NonReadonlyMemberInGetHashCode

namespace Protocol
{
    /// <summary>
    /// 类型：类
    /// 名称：SocketGuid
    /// 作者：taixihuase
    /// 作用：基于套接字全局唯一标识符类
    /// 编写日期：2016/1/25
    /// </summary>
    [ProtoContract]
    public class SocketGuid
    {
        [ProtoMember(1)]
        public int IpAddress { get; protected set; }

        [ProtoMember(2)]
        public ushort Port { get; protected set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof (SocketGuid))
                return false;

            SocketGuid key = obj as SocketGuid;
            return key.IpAddress == IpAddress && key.Port == Port;
        }

        public override int GetHashCode()
        {
            return GetSocketToString().GetHashCode();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SocketGuid
        /// 作者：taixihuase
        /// 作用：构造 SocketGuid 对象
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public SocketGuid(string ip = null, ushort port = 0)
        {
            IpAddress = ip != null ? IpTool.ConvertIpToInt32(ip) : 0;
            Port = port;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SocketGuid
        /// 作者：taixihuase
        /// 作用：构造 SocketGuid 对象
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="ip"></param>
        public SocketGuid(IPEndPoint ip)
        {
            IpAddress = ip != null ? IpTool.ConvertIpToInt32(ip.Address.ToString()) : 0;
            Port = ip != null ? (ushort) ip.Port : (ushort) 0;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SocketGuid
        /// 作者：taixihuase
        /// 作用：默认构造函数，构造 SocketGuid 对象
        /// 编写日期：2016/1/25
        /// </summary>
        public SocketGuid()
        {
            IpAddress = 0;
            Port = 0;
        } 

        /// <summary>
        /// 类型：方法
        /// 名称：GetSocket
        /// 作者：taixihuase
        /// 作用：获得标识符所使用的套接字
        /// 编写日期：2016/1/25
        /// </summary>
        /// <returns></returns>
        public IPEndPoint GetSocket()
        {
            return new IPEndPoint(IpAddress, Port);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetSocketToString
        /// 作者：taixihuase
        /// 作用：获得标识符所使用的套接字的字符串表示
        /// 编写日期：2016/2/2
        /// </summary>
        /// <returns></returns>
        public string GetSocketToString()
        {
            return $"{IpTool.ConvertIpToString(IpAddress)}:{Port}";
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetIpToString
        /// 作者：taixihuase
        /// 作用：获得标识符包含的网络地址的字符串表示
        /// 编写日期：2016/2/11
        /// </summary>
        /// <returns></returns>
        public string GetIpToString()
        {
            return IpTool.ConvertIpToString(IpAddress);
        }
    }
}
