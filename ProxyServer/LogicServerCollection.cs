//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：LogicServerCollection.cs
//
// 文件功能描述：
//
// 逻辑服务器集合
//
// 创建标识：taixihuase 20160213
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

namespace ProxyServer
{
    /// <summary>
    /// 类型：类
    /// 名称：LogicServerCollection
    /// 作者：taixihuase
    /// 作用：保存逻辑服务器信息
    /// 编写日期：2016/2/13
    /// </summary>
    public class LogicServerCollection
    {
        public static readonly LogicServerCollection Instance = new LogicServerCollection();

        public Dictionary<SocketGuid, PeerToLogicServer> GuidToLogicServer { get; protected set; }

        public List<SocketGuid> GuidOfDefaultLogicServer { get; protected set; }

        public bool LogicReady { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：LogicServerCollection
        /// 作者：taixihuase
        /// 作用：构造逻辑服务器集合对象
        /// 编写日期：2016/2/13
        /// </summary>
        private LogicServerCollection()
        {
            GuidToLogicServer = new Dictionary<SocketGuid, PeerToLogicServer>();
            GuidOfDefaultLogicServer = new List<SocketGuid>();
            SetReady(false);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AddLogicServer
        /// 作者：taixihuase
        /// 作用：添加一个逻辑服务器信息
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="logicPeer"></param>
        /// <returns></returns>
        public bool AddLogicServer(SocketGuid serverId, PeerToLogicServer logicPeer)
        {
            lock (this)
            {
                if (GuidToLogicServer.ContainsKey(serverId)) return false;

                SetReady(false);
                GuidToLogicServer.Add(serverId, logicPeer);
                SetReady(true);
                return true;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveLogicServer
        /// 作者：taixihuase
        /// 作用：移除一个指定的逻辑服务器信息
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        public bool RemoveLogicServer(SocketGuid serverId)
        {
            lock (this)
            {
                if (!GuidToLogicServer.ContainsKey(serverId)) return false;

                SetReady(false);
                RemoveDefaultLogicServer(serverId);
                GuidToLogicServer.Remove(serverId);
                SetReady(true);

                return true;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AddDefaultLogicServer
        /// 作者：taixihuase
        /// 作用：添加一个默认逻辑服务器编号
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        private void AddDefaultLogicServer(SocketGuid serverId)
        {
            if (GuidOfDefaultLogicServer.Contains(serverId))
                return;

            GuidOfDefaultLogicServer.Add(serverId);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveDefaultLogicServer
        /// 作者：taixihuase
        /// 作用：移除一个指定的默认逻辑服务器编号
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="serverId"></param>
        /// <returns></returns>
        private void RemoveDefaultLogicServer(SocketGuid serverId)
        {
            if (!GuidOfDefaultLogicServer.Contains(serverId))
                return;

            GuidOfDefaultLogicServer.Remove(serverId);
        }


        /// <summary>
        /// 类型：方法
        /// 名称：UpdateDefaultLogicServers
        /// 作者：taixihuase
        /// 作用：更新默认逻辑服务器编号列表
        /// 编写日期：2016/2/13
        /// </summary>
        /// <param name="servers"></param>
        /// <returns></returns>
        public bool UpdateDefaultLogicServers(List<SocketGuid> servers)
        {
            lock (this)
            {
                if (!servers.Except(GuidOfDefaultLogicServer).Any() &&
                    !GuidOfDefaultLogicServer.Except(servers).Any())
                    return false;

                SetReady(false);
                GuidOfDefaultLogicServer.Clear();
                foreach (var server in servers)
                {
                    AddDefaultLogicServer(server);
                }
                SetReady(true);

                return true;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetReady
        /// 作者：taixihuase
        /// 作用：设置逻辑服务器集合的可用状态
        /// 编写日期：2016/2/14
        /// </summary>
        /// <param name="ready"></param>
        public void SetReady(bool ready)
        {
            LogicReady = ready;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Clear
        /// 作者：taixihuase
        /// 作用：清空逻辑服务器集合并置为不可用状态
        /// 编写日期：2016/2/21
        /// </summary>
        public void Clear()
        {
            lock (this)
            {
                SetReady(false);
                foreach (var server in GuidToLogicServer)
                {
                    server.Value.Dispose();
                }
                GuidOfDefaultLogicServer.Clear();
                GuidToLogicServer.Clear();
            }
        }
    }
}
