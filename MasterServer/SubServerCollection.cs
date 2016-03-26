//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：SubServerCollection.cs
//
// 文件功能描述：
//
// 子服务器集合
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

using System.Collections.Generic;
using System.Linq;
using Protocol;
using S2SProtocol.Common;

// ReSharper disable ReturnValueOfPureMethodIsNotUsed

namespace MasterServer
{

    /// <summary>
    /// 类型：类
    /// 名称：SubServerCollection
    /// 作者：taixihuase
    /// 作用：保存子服务器信息
    /// 编写日期：2016/1/25
    /// </summary>
    public class SubServerCollection
    {
        public static readonly SubServerCollection Instance = new SubServerCollection();

        public Dictionary<SocketGuid, PeerToSubServer> GuidToServer { get; protected set; }
        
        public Dictionary<ServerType, List<SocketGuid>> TypeToServers { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：SubServerCollection
        /// 作者：taixihuase
        /// 作用：构造子服务器集合对象
        /// 编写日期：2016/1/25
        /// </summary>
        private SubServerCollection()
        {
            GuidToServer = new Dictionary<SocketGuid, PeerToSubServer>();
            TypeToServers = new Dictionary<ServerType, List<SocketGuid>>();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AddSubServer
        /// 作者：taixihuase
        /// 作用：添加一个子服务器信息
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="serverPeer"></param>
        /// <returns></returns>
        public bool AddSubServer(SocketGuid serverId, PeerToSubServer serverPeer)
        {
            lock (this)
            {
                if (GuidToServer.ContainsKey(serverId)) return false;

                GuidToServer.Add(serverId, serverPeer);
                if (!TypeToServers.ContainsKey(serverPeer.Type))
                    TypeToServers.Add(serverPeer.Type, new List<SocketGuid>());
                TypeToServers[serverPeer.Type].Add(serverId);

                return true;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveServer
        /// 作者：taixihuase
        /// 作用：移除指定的子服务器
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="serverId"></param>
        public void RemoveSubServer(SocketGuid serverId)
        {
            lock (this)
            {
                if (GuidToServer.ContainsKey(serverId))
                {
                    ServerType t = GuidToServer[serverId].Type;
                    TypeToServers[t].Remove(serverId);
                    if (TypeToServers[t].Count == 0)
                        TypeToServers.Remove(t);
                                   
                    GuidToServer.Remove(serverId);
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetServerType
        /// 作者：taixihuase
        /// 作用：尝试获取指定的子服务器的服务器类型
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="serverType"></param>
        /// <returns></returns>
        public bool TryGetServerType(SocketGuid serverId, out ServerType serverType)
        {
            if (GuidToServer.ContainsKey(serverId))
            {
                serverType = GuidToServer[serverId].Type;
                return true;
            }
            serverType = ServerType.UndefinedServer;
            return false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetServerPeers
        /// 作者：taixihuase
        /// 作用：尝试获取指定服务器类型的子服务器列表
        /// 编写日期：2016/1/25
        /// </summary>
        /// <param name="type"></param>
        /// <param name="peers"></param>
        /// <returns></returns>
        public bool TryGetServerPeers(ServerType type, out List<PeerToSubServer> peers)
        {
            lock (this)
            {
                peers = new List<PeerToSubServer>();
                if (!TypeToServers.ContainsKey(type)) return false;
                peers.AddRange(TypeToServers[type].Select(socketGuid => GuidToServer[socketGuid]));
                return true;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetLogicServerPeers
        /// 作者：taixihuase
        /// 作用：尝试获取逻辑服务器列表
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="logic"></param>
        /// <returns></returns>
        public bool TryGetLogicServerPeers(out List<PeerToSubServer> logic)
        {
            return TryGetServerPeers(ServerType.LogicServer, out logic);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetProxyServerPeers
        /// 作者：taixihuase
        /// 作用：尝试获取网关代理服务器列表
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public bool TryGetProxyServerPeers(out List<PeerToSubServer> proxy)
        {
            return TryGetServerPeers(ServerType.ProxyServer, out proxy);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OrderServerPeersByLoadAsc
        /// 作者：taixihuase
        /// 作用：按负载升序排序服务器列表
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="servers"></param>
        public void OrderServerPeersByLoadAsc(ref List<PeerToSubServer> servers)
        {
            servers = servers?.OrderBy(s => s.CpuLoad).ToList();           
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OrderServerPeersByLoadDesc
        /// 作者：taixihuase
        /// 作用：按负载降序排序服务器列表
        /// 编写日期：2016/2/3
        /// </summary>
        /// <param name="servers"></param>
        public void OrderServerPeersByLoadDesc(ref List<PeerToSubServer> servers)
        {
            servers = servers?.OrderByDescending(s => s.CpuLoad).ToList();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetDefaultLogicForProxy
        /// 作者：taixihuase
        /// 作用：向网关代理服务器配置默认逻辑服务器列表
        /// 编写日期：2016/3/7
        /// </summary>
        /// <param name="proxys"></param>
        /// <param name="defaultLogics"></param>
        public bool SetDefaultLogicForProxy(out List<PeerToSubServer> proxys, out List<List<SocketGuid>> defaultLogics)
        {
            List<PeerToSubServer> logics;
            if (TryGetProxyServerPeers(out proxys) &&
                TryGetLogicServerPeers(out logics))
            {
                OrderServerPeersByLoadAsc(ref proxys);
                OrderServerPeersByLoadDesc(ref logics);

                defaultLogics = new List<List<SocketGuid>>();
                int lnum = logics.Count;
                int pnum = proxys.Count;
                for (int i = 0; i < pnum; i++)
                    defaultLogics.Add(new List<SocketGuid>());

                if (lnum >= pnum)
                {                  
                    int avg = lnum / pnum;
                    int remainder = lnum % pnum;
                    for (int i = 0; i < avg; i++)
                        for (int j = 0; j < pnum; j++)
                            defaultLogics[j].Add(logics[j + i * pnum].Guid);
                    for (int i = 0; i < remainder; i++)
                        defaultLogics[i].Add(logics[i + lnum - remainder].Guid);
                }
                else
                {
                    for (int i = 0; i < lnum; i++)
                        defaultLogics[i].Add(logics[i].Guid);
                    int cnt = 0;
                    for (int i = pnum - 1; i >= lnum; i--)
                    {
                        defaultLogics[i].Add(logics[lnum - 1 - cnt].Guid);
                        if (++cnt >= lnum)
                            cnt = 0;
                    }
                }
                return true;
            }
            defaultLogics = null;
            return false;
        }
    }
}
