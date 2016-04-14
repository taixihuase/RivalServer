//-----------------------------------------------------------------------------------------------------------
// Copyright (C) 2016-2017 RivalRival
// 版权所有
//
// 文件名：CaptchaCollection.cs
//
// 文件功能描述：
//
// 用于生成、保存和验证注册账号时发送到用户邮箱的验证码
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

using System;
using System.Collections.Generic;
using C2SProtocol.Data;
using ExitGames.Concurrency.Fibers;
using S2SProtocol.Common;

namespace MasterServer.LoginServerRequest
{
    /// <summary>
    /// 类型：类
    /// 名称：CaptchaCollection
    /// 作者：taixihuase
    /// 作用：验证码集合类
    /// 编写日期：2016/3/17
    /// </summary>
    public class CaptchaCollection
    {
        public static readonly CaptchaCollection Instance = new CaptchaCollection();
       
        private Dictionary<string, string> Captcha { get; set; }

        public ExtendedPoolFiber Fiber { get; set; }

        /// <summary>
        /// 类型：方法
        /// 名称：CaptchaCollection
        /// 作者：taixihuase
        /// 作用：默认构造验证码集合类实例
        /// 编写日期：2016/3/17
        /// </summary>
        private CaptchaCollection()
        {
            Captcha = new Dictionary<string, string>();
            Fiber = new ExtendedPoolFiber();
            Fiber.Start();
        }

        /// <summary>
        /// 类型：方法
        /// 名称：CreateRandomCaptcha
        /// 作者：taixihuase
        /// 作用：生成指定长度的随机数字验证码字符串
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CreateRandomCaptcha(int length)
        {           
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";

            //生成起始序列值
            int seekSeek = unchecked((int)ServerTime.Instance.Time.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = seekRand.Next(0, int.MaxValue - length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }

            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, int.MaxValue);
            }

            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }

            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：GetCaptcha
        /// 作者：taixihuase
        /// 作用：通过邮箱获取其验证码，若无有效验证码则返回 null
        /// 编写日期：2016/3/18
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string GetCaptcha(string email)
        {
            if (Captcha.ContainsKey(email))
                return Captcha[email];
            return null;
        }

        /// <summary>
        /// 类型：方法
        /// 名称：AddCaptchaWithEmail
        /// 作者：taixihuase
        /// 作用：添加邮箱与验证码对，并设置验证码有效时长
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captcha"></param>
        /// <param name="lifetime"></param>
        private void AddCaptchaWithEmail(string email, string captcha, long lifetime)
        {
            lock (this)
            {
                if (!Captcha.ContainsKey(email))
                {
                    Captcha.Add(email, captcha);
                    Fiber.Schedule((() => RemoveCaptchWithEmail(email, captcha)), lifetime);
                }
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：RemoveCaptchaWithEmail
        /// 作者：taixihuase
        /// 作用：移除完全匹配的邮箱与验证码对
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captcha"></param>
        public void RemoveCaptchWithEmail(string email, string captcha)
        {
            lock (this)
            {
                if (Captcha.ContainsKey(email) && Captcha[email] == captcha)
                    Captcha.Remove(email);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：UpdateCaptchaWithEmail
        /// 作者：taixihuase
        /// 作用：更新邮箱与验证码对，并重设验证码有效时长，若未包含该邮箱，则添加一个邮箱与验证码对
        /// 编写日期：2016/3/17
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captcha"></param>
        /// <param name="lifetime"></param>
        public void UpdateCaptchaWithEmail(string email, string captcha, long lifetime = 300000)
        {
            lock (this)
            {
                if (Captcha.ContainsKey(email))
                {
                    if (Captcha[email] != captcha)
                    {
                        Captcha[email] = captcha;
                        Fiber.Schedule((() => RemoveCaptchWithEmail(email, captcha)), lifetime);
                    }
                    return;
                }
                AddCaptchaWithEmail(email, captcha, lifetime);
            }
        }

        /// <summary>
        /// 类型：方法
        /// 名称：IsCaptchaMatch
        /// 作者：taixihuase
        /// 作用：判断邮箱与验证码是否匹配
        /// 编写日期：2016/3/18
        /// </summary>
        /// <param name="email"></param>
        /// <param name="captcha"></param>
        /// <returns></returns>
        public RegistInfo.CheckStatus IsCaptchaMatch(string email, string captcha)
        {
            if (!Captcha.ContainsKey(email)) return RegistInfo.CheckStatus.CaptchaNotObtained;
            return Captcha[email] == captcha ? RegistInfo.CheckStatus.CaptchaMatched : RegistInfo.CheckStatus.CaptchaNotMatched;
        }
    }
}
