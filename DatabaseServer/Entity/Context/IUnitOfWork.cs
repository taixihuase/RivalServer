//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：IUnitOfWork.cs
//
// 文件功能描述：
//
// 工作单元接口
//
// 创建标识：taixihuase 20160403
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//----------------------------------------------------------------------------------------------------------

using System.Data.Entity;

namespace DatabaseServer.Entity.Context
{
    /// <summary>
    /// 类型：接口
    /// 名称：IUnitOfWork
    /// 作者：taixihuase
    /// 作用：工作单元接口
    /// 编写日期：2016/4/3
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 类型：方法
        /// 名称：Find
        /// 作者：taixihuase
        /// 作用：查找目标类型的实体
        /// 编写日期：2016/4/3
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IDbSet<T> Find<T>() where T : class;

        /// <summary>
        /// 类型：方法
        /// 名称：Refresh
        /// 作者：taixihuase
        /// 作用：刷新本地缓存数据
        /// 编写日期：2016/4/3
        /// </summary>
        void Refresh();

        /// <summary>
        /// 类型：方法
        /// 名称：Commit
        /// 作者：taixihuase
        /// 作用：提交操作
        /// 编写日期：2016/4/3
        /// </summary>
        void Commit();
    }
}
