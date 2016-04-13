//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：UserCollection.cs
//
// 文件功能描述：
//
// 用于记录用户的基本账户数据
//
// 创建标识：taixihuase 20160317
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
using C2SProtocol.Data;
using ExitGames.Concurrency.Fibers;

namespace MasterServer.DataCollection
{
    /// <summary>
    /// 类型：类
    /// 名称：UserCollection
    /// 作者：taixihuase
    /// 作用：用户数据集合类
    /// 编写日期：2016/3/17
    /// </summary>
    public class UserCollection
    {
        public static readonly UserCollection Instance = new UserCollection();

        public ExtendedPoolFiber Fiber { get; set; }

        private Dictionary<int, UserInfo> Users { get; set; }

        // ReSharper disable once InconsistentNaming
        private Dictionary<string, int> AccountToID { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：UserCollection
        /// 作者：taixihuase
        /// 作用：默认构造用户基本数据集合类实例
        /// 编写日期：2016/3/17
        /// </summary>
        private UserCollection()
        {
            Fiber = new ExtendedPoolFiber();
            Users = new Dictionary<int, UserInfo>();
            AccountToID = new Dictionary<string, int>();
            Fiber.Start();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AddUser
        /// 作者：taixihuase
        /// 作用：添加一名用户
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUser(UserInfo user)
        {
            if (!Users.ContainsKey(user.UniqueId))
            {
                lock (this)
                {
                    Users.Add(user.UniqueId, user);
                    AccountToID.Add(user.Account, user.UniqueId);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveUser
        /// 作者：taixihuase
        /// 作用：移除一名用户
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool RemoveUser(UserInfo user)
        {
            return RemoveUser(user.UniqueId);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveUser
        /// 作者：taixihuase
        /// 作用：通过编号移除一名用户
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveUser(int id)
        {
            if (!Users.ContainsKey(id))
                return false;
            string account = Users[id].Account;
            RemoveUser(account);
            return true;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveUser
        /// 作者：taixihuase
        /// 作用：通过账号移除一名用户
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool RemoveUser(string account)
        {
            if (!AccountToID.ContainsKey(account)) return false;
            lock (this)
            {
                Users.Remove(AccountToID[account]);
                AccountToID.Remove(account);
            }
            return true;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetUser
        /// 作者：taixihuase
        /// 作用：尝试通过编号获取一名用户信息
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool TryGetUser(int id, out UserInfo user)
        {
            if (!Users.ContainsKey(id))
            {
                user = null;
                return false;
            }
            user = Users[id];
            return true;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：TryGetUser
        /// 作者：taixihuase
        /// 作用：尝试通过账号获取一名用户信息
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="account"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool TryGetUser(string account, out UserInfo user)
        {
            if (IsUserExist(account) == RegistInfo.CheckStatus.EmailNotExist)
            {
                user = null;
                return false;
            }
            user = Users[AccountToID[account]];
            return true;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsUserExist
        /// 作者：taixihuase
        /// 作用：判断账号是否存在
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public RegistInfo.CheckStatus IsUserExist(string account)
        {
            return AccountToID.ContainsKey(account) ? RegistInfo.CheckStatus.EmailExist : RegistInfo.CheckStatus.EmailNotExist;
        }
    }
}
