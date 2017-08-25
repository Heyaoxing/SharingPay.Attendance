using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Yunpian.conf;
using Yunpian.lib;
using Yunpian.model;

namespace Yunpian
{
    public class Push
    {
        UserOperator user;
        Config config;
        Dictionary<string, string> data = new Dictionary<string, string>();
        public Push()
        {

            //设置apikey
             config = new Config("");
            Result result = null;
        }


        /// <summary>
        /// 早上忘记打卡通知
        /// </summary>
        public void Morning(List<string> mobiles)
        {
            if (!mobiles.Any())
            {
                return;
            }

            try
            {
                var date = DateTime.Now.ToString("MM月dd日 HH:mm:ss");
                // 发送单条短信
                SmsOperator sms = new SmsOperator(config);
                data.Clear();
                data.Add("mobile", string.Join(",", mobiles));
                data.Add("text", $"【秋刀鱼科技】亲！您截止至{date}时未有打卡记录!该打卡了!么么哒");
                sms.batchSend(data);
            }
            catch (Exception exception)
            {
                Log.Error("早上忘记打卡通知异常:" + exception.Message);
            }

        }


        /// <summary>
        /// 早上迟到通知
        /// </summary>
        public void MorningLate(Dictionary<string, DateTime> dictionary)
        {
            if (!dictionary.Any())
            {
                return;
            }

            try
            {
                foreach (var item in dictionary)
                {
                    // 发送单条短信
                    SmsOperator sms = new SmsOperator(config);
                    data.Clear();
                    data.Add("mobile", item.Key);
                    data.Add("text", $"【秋刀鱼科技】亲！您迟到了哟！您的上班打开时间为{item.Value}。30块!可以吃多少顿麻辣烫!可以买多少个泡泡糖!");
                    sms.singleSend(data);
                }

            }
            catch (Exception exception)
            {
                Log.Error("早上迟到通知:" + exception.Message);
            }
        }

        /// <summary>
        /// 下午忘记打卡通知
        /// </summary>
        public void Afternoon(List<string> mobiles)
        {
            if (!mobiles.Any())
            {
                return;
            }

            try
            {
                var date = DateTime.Now.ToString("MM月dd日 HH:mm:ss");
                // 发送单条短信
                SmsOperator sms = new SmsOperator(config);
                data.Clear();
                data.Add("mobile", string.Join(",", mobiles));
                data.Add("text", $"【秋刀鱼科技】亲！您截止至{date}时未有下班打卡记录！快去打卡！");
                sms.batchSend(data);
            }
            catch (Exception exception)
            {
                Log.Error("下午忘记打卡通知异常:" + exception.Message);
            }
        }


        /// <summary>
        /// 中午忘记打卡通知
        /// </summary>
        public void Noon(Dictionary<string,int> dictionary)
        {
            if (!dictionary.Any())
            {
                return;
            }

            try
            {
                var date = DateTime.Now.ToString("MM月dd日 HH:mm:ss");
                foreach (var item in dictionary)
                {
                    // 发送单条短信
                    SmsOperator sms = new SmsOperator(config);
                    data.Clear();
                    data.Add("mobile", item.Key);
                    data.Add("text", $"【秋刀鱼科技】大哥大姐！您截止至{date}中午时仅打卡{item.Value}次!中午要打2次卡！该打卡了！");
                    sms.singleSend(data);
                }
                
            }
            catch (Exception exception)
            {
                Log.Error("中午忘记打卡通知:" + exception.Message);
            }
        }
    }
}
