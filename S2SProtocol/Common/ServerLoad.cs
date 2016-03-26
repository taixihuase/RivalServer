//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 Rival
// 版权所有
//
// 文件名：ServerLoad.cs
//
// 文件功能描述：
//
// 子服务器负载情况
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

using System;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using ProtoBuf;

namespace S2SProtocol.Common
{
    /// <summary>
    /// 类型：类
    /// 名称：ServerLoad
    /// 作者：taixihuase
    /// 作用：计算子服务器的处理器和内存的负载情况
    /// 编写日期：2016/1/25
    /// </summary>
    [ProtoContract]
    public class ServerLoad
    {
        public static int CountTime { get; } = 10;

        [ProtoMember(1)]
        public float CpuLoad { get; set; }

        public float MemoryLoad { get; set; }

        /// <summary>
        /// 类型：枚举
        /// 名称：LoadLevel
        /// 作者：taixihuase
        /// 作用：子服务器负载水平枚举
        /// 编写日期：2016/1/30
        /// </summary>
        public enum LoadLevel
        {
            Undetected = 0,
            Lowest = 10,
            Low = 30,
            Normal = 50,
            High = 70,
            Highest = 90
        }

        [ProtoMember(2)]
        public LoadLevel ServerLoadLevel { get; protected set; }

        /// <summary>
        /// 类型：方法
        /// 名称：Clear
        /// 作者：taixihuase
        /// 作用：清空负载记录
        /// 编写日期：2016/3/8
        /// </summary>
        public void Clear()
        {
            CpuLoad = 0;
            MemoryLoad = 0;
            ServerLoadLevel = LoadLevel.Undetected;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetMemoryLoad
        /// 作者：taixihuase
        /// 作用：获取子服务器内存的负载情况
        /// 编写日期：2016/1/25
        /// </summary>
        public void GetMemoryLoad()
        {
            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (var o in moc)
            {
                long totalBytes = 0;
                long availableBytes = 0;
                var mo = (ManagementObject) o;
                if (mo["TotalVisibleMemorySize"] != null)
                {
                    totalBytes = long.Parse(mo["TotalVisibleMemorySize"].ToString());
                }
                if (mo["FreePhysicalMemory"] != null)
                {
                    availableBytes = long.Parse(mo["FreePhysicalMemory"].ToString());
                }

                MemoryLoad = (totalBytes - availableBytes)*100.0f/totalBytes;
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetCpuLoad
        /// 作者：taixihuase
        /// 作用：获取子服务器处理器的负载情况
        /// 编写日期：2016/1/25
        /// </summary>
        public void GetCpuLoad()
        {
            PerformanceCounterFun();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：PerformanceCounterFun
        /// 作者：taixihuase
        /// 作用：计算一段时间内子服务器处理器的平均负载情况
        /// 编写日期：2016/1/25
        /// </summary>
        private void PerformanceCounterFun()
        {
            PerformanceCounter[] counters = new PerformanceCounter[Environment.ProcessorCount];
            for (int i = 0; i < counters.Length; i++)
            {
                counters[i] = new PerformanceCounter("Processor", "% Processor Time", i.ToString());
                counters[i].NextValue();
            }
            Thread.Sleep(1000);
            float load = 0;
            for (int i = 0; i < CountTime; i++)
            {
                load += counters.Sum(t => t.NextValue());
                Thread.Sleep(1000);
            }
            CpuLoad = load/(CountTime*counters.Length);
            SetLoadLevel();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：SetLoadLevel
        /// 作者：taixihuase
        /// 作用：设置服务器负载程度值
        /// 编写日期：2016/1/30
        /// </summary>
        public void SetLoadLevel()
        {
            lock (this)
            {
                if (Math.Abs(CpuLoad) <= 0.01f)
                {
                    ServerLoadLevel = LoadLevel.Undetected;
                }
                if (CpuLoad < (int) LoadLevel.Lowest)
                {
                    ServerLoadLevel = LoadLevel.Lowest;
                }
                else if (CpuLoad < (int) LoadLevel.Low)
                {
                    ServerLoadLevel = LoadLevel.Low;
                }
                else if (CpuLoad < (int) LoadLevel.Normal)
                {
                    ServerLoadLevel = LoadLevel.Normal;
                }
                else if (CpuLoad < (int) LoadLevel.High)
                {
                    ServerLoadLevel = LoadLevel.High;
                }
                else
                {
                    ServerLoadLevel = LoadLevel.Highest;
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetServerLoad
        /// 作者：taixihuase
        /// 作用：获取服务器负载
        /// 编写日期：2016/2/2
        /// </summary>
        public void GetServerLoad()
        {
            Clear();
            GetMemoryLoad();
            GetCpuLoad();
        }
    }
}
