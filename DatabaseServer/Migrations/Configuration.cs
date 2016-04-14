//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// ��Ȩ����
//
// �ļ�����Configuration.cs
//
// �ļ�����������
//
// �������ݿ�����
//
// ������ʶ��taixihuase 20160403
//
// �޸ı�ʶ��
// �޸�������
// 
//
// �޸ı�ʶ��
// �޸�������
//
//----------------------------------------------------------------------------------------------------------

using System.Data.Entity.Migrations;
using DatabaseServer.Entity.Context;

namespace DatabaseServer.Migrations
{
    /// <summary>
    /// ���ͣ���
    /// ���ƣ�Configuration
    /// ���ߣ�taixihuase
    /// ���ã����ݿ�������
    /// ��д���ڣ�2016/4/3
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<RivalContext>
    {
        /// <summary>
        /// ���ͣ�����
        /// ���ƣ�Configuration
        /// ���ߣ�taixihuase
        /// ���ã�Ĭ�Ϲ������ݿ�������
        /// ��д���ڣ�2016/4/3
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DatabaseServer.Entity.Context.RivalContext";
        }

        /// <summary>
        /// ���ͣ�����
        /// ���ƣ�Seed
        /// ���ߣ�taixihuase
        /// ���ã�ÿ��Ǩ��ʱ����ʼ�����ݿ������ģ����ֶ�Ǩ�ƻ���ø÷���
        /// ��д���ڣ�2016/4/3
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(RivalContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
