//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：RivalContext.cs
//
// 文件功能描述：
//
// Rival 数据库上下文
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
using System.Linq;
using DatabaseServer.Entity.Maps;
using DatabaseServer.Entity.Models;

namespace DatabaseServer.Entity.Context
{
    /// <summary>
    /// 类型：类
    /// 名称：RivalContext
    /// 作者：taixihuase
    /// 作用：数据库上下文
    /// 编写日期：2016/4/3
    /// </summary>
    public class RivalContext : DbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Player> Players { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet<Title> Titles { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<CardEffect> CardEffects { get; set; }

        public DbSet<Deck> Decks { get; set; } 

        public DbSet<CardPack> CardPacks { get; set; } 

        /// <summary>
        /// 类型：方法
        /// 名称：RivalContext
        /// 作者：taixihuase
        /// 作用：构造数据库上下文对象
        /// 编写日期：2016/4/3
        /// </summary>
        public RivalContext() : base("Rival")
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RivalContext
        /// 作者：taixihuase
        /// 作用：构造数据库上下文对象
        /// 编写日期：2016/4/3
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public RivalContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        /// <summary>
        /// 类型：方法
        /// 名称：OnModelCreating
        /// 作者：taixihuase
        /// 作用：配置实体映射
        /// 编写日期：2016/4/3
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations
                .Add(new UserMap())
                .Add(new PlayerMap())
                .Add(new AvatarMap())
                .Add(new TitleMap())
                .Add(new LevelMap())
                .Add(new CardMap())
                .Add(new CombatAttributeMap())
                .Add(new CardEffectMap())
                .Add(new DeckMap())
                .Add(new CardPackMap())
                .Add(new PlayerCardPackMap());
            
            base.OnModelCreating(modelBuilder);
        }

        #region IUnitOfWork methods

        public IDbSet<T> Find<T>() where T : class
        {
            return Set<T>();
        }

        public void Refresh()
        {
            try
            {
                ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
            }
            catch
            {
                // ignored
            }
        }

        public void Commit()
        {
            SaveChanges();
        }

        #endregion

    }
}
