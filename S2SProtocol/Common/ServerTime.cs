//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ServerTime.cs
//
// 文件功能描述：
//
// 获取、记录和更新服务器时间
//
// 创建标识：taixihuase 20160414
//
// 修改标识：
// 修改描述：
// 
//
// 修改标识：
// 修改描述：
//
//-----------------------------------------------------------------------------------------------------------

using System;
using System.Threading;
using ExitGames.Concurrency.Fibers;
using Protocol;

namespace S2SProtocol.Common
{
    /// <summary>
    /// 类型：类
    /// 名称：ServerTime
    /// 作者：taixihuase
    /// 作用：服务器时间类
    /// 编写日期：2016/4/14
    /// </summary>
    public class ServerTime
    {
        public static readonly ServerTime Instance = new ServerTime();

        private readonly ExtendedPoolFiber _fiber;

        private DateTime _time;

        public DateTime Time
        {
            get
            {
                return _start ? _time : DateTime.MinValue;
            }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _time = value;
            }
        }

        private bool _start;

        private const long Duration = 500;

        /// <summary>
        /// 类型：方法
        /// 名称：ServerTime
        /// 作者：taixihuase
        /// 作用：构造服务器时间类实例
        /// 编写日期：2016/4/14
        /// </summary>
        private ServerTime()
        {
            _start = false;
            _fiber = new ExtendedPoolFiber();
            _fiber.Start();
            _fiber.Enqueue(Start);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Start
        /// 作者：taixihuase
        /// 作用：获取服务器时间
        /// 编写日期：2016/4/14
        /// </summary>
        private void Start()
        {
            while (NetTool.IsConnectedToInternet() == false)
            {
                Console.WriteLine("Connect to Network Failed");
                Thread.Sleep(50);
            }
            _start = true;
            Time = TimeTool.GetBeijingTime();
            _fiber.ScheduleOnInterval(Update, Duration, Duration);
        }

        /// <summary>
        /// 类型：方法
        /// 名称：Update
        /// 作者：taixihuase
        /// 作用：更新服务器时间
        /// 编写日期：2016/4/14
        /// </summary>
        private void Update()
        {
            Time = Time.AddMilliseconds(Duration);
        }
    }
}