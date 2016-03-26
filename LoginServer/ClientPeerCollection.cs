//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ClientPeerCollection.cs
//
// 文件功能描述：
//
// 用户连接集合，存放和操作连线中的客户端信息
//
// 创建标识：taixihuase 20160315
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using Protocol;

namespace LoginServer
{
    /// <summary>
    /// 类型：类
    /// 名称：ClientPeerCollection
    /// 作者：taixihuase
    /// 作用：保存当前连接的客户端，访问用户信息
    /// 编写日期：2016/3/15
    /// </summary>
    public class ClientPeerCollection
    {
        public static readonly ClientPeerCollection Instance = new ClientPeerCollection();

        private Dictionary<SocketGuid, PeerToClient> ConnectedClients { get; set; }

        private Dictionary<PeerToClient, SocketGuid> PeerToGuid { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：ClientPeerCollection
        /// 作者：taixihuase
        /// 作用：构造 ClientPeerCollection 对象
        /// 编写日期：2016/3/15
        /// </summary>
        private ClientPeerCollection()
        {
            ConnectedClients = new Dictionary<SocketGuid, PeerToClient>();
            PeerToGuid = new Dictionary<PeerToClient, SocketGuid>();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AddConnectedPeer
        /// 作者：taixihuase
        /// 作用：添加一个新的客户端连接
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="peer"></param>
        /// <returns></returns>
        public bool AddConnectedPeer(SocketGuid guid, PeerToClient peer)
        {
            lock (this)
            {
                if (!ConnectedClients.ContainsKey(guid))
                {
                    ConnectedClients.Add(guid, peer);
                    PeerToGuid.Add(peer, guid);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetPeer
        /// 作者：taixihuase
        /// 作用：尝试通过套接字编号获取客户端连接
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public PeerToClient TryGetPeer(SocketGuid guid)
        {
            if (ConnectedClients.ContainsKey(guid))
            {
                PeerToClient peer;
                ConnectedClients.TryGetValue(guid, out peer);
                return peer;
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetSocketGuid
        /// 作者：taixihuase
        /// 作用：尝试通过客户端 Peer 获取套接字编号
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public SocketGuid TryGetSocketGuid(PeerToClient client)
        {
            if (PeerToGuid.ContainsKey(client))
            {
                SocketGuid guid;
                PeerToGuid.TryGetValue(client, out guid);
                return guid;
            }
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemovePeer
        /// 作者：taixihuase
        /// 作用：通过套接字编号删除一个客户端连接
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns> 
        public bool RemovePeer(SocketGuid guid)
        {
            lock (this)
            {
                if (ConnectedClients.ContainsKey(guid))
                {
                    RemovePeer(TryGetPeer(guid));
                    ConnectedClients.Remove(guid);
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemovePeer
        /// 作者：taixihuase
        /// 作用：删除一个客户端连接
        /// 编写日期：2016/3/15
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns> 
        private bool RemovePeer(PeerToClient client)
        {
            lock (this)
            {
                if (client != null && PeerToGuid.ContainsKey(client))
                {
                    PeerToGuid.Remove(client);
                    return true;
                }
                return false;
            }
        }
    }
}
