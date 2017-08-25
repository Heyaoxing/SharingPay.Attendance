using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharingPay.Attendance.Data;
using Yunpian;

namespace SharingPay.Attendance.Rule
{
    public class AttendanceRule
    {
        /// <summary>
        /// 上午上班打卡提醒
        /// </summary>
        public static void Morning()
        {
            var model = Repository.GetAttendanceCaches();
            var person = Repository.GetPerson();

            List<string> none = new List<string>();
            Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();
            var now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, now.Day, 8, 0, 0);
            var middle = new DateTime(now.Year, now.Month, now.Day, 8, 31, 0);
            foreach (var item in person)
            {
                var exists = model.Exists(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn >= start && p.AttendancedOn <= now);
                if (!exists)
                {
                    none.Add(item.Phone);
                }

                if (!model.Exists(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn <= middle) && model.Exists(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn >= middle && p.AttendancedOn <= now))
                {
                    var late = model.Where(p => p.EnrollNumber == item.EnrollNumber).OrderBy(p => p.AttendancedOn).FirstOrDefault();
                    dictionary.Add(item.Phone, late.AttendancedOn);
                }
            }
            Push push = new Push();

            push.Morning(none);
            push.MorningLate(dictionary);
        }

        /// <summary>
        ///上午迟到打卡提醒
        /// </summary>
        public static void MorningLate()
        {
            var model = Repository.GetAttendanceCaches();
            var person = Repository.GetPerson();

            Dictionary<string, DateTime> dictionary = new Dictionary<string, DateTime>();
            var now = DateTime.Now;
            var middle = new DateTime(now.Year, now.Month, now.Day, 8, 31, 0);
            foreach (var item in person)
            {
              
                if (!model.Exists(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn <= middle) && model.Exists(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn >= middle && p.AttendancedOn <= now))
                {
                    var late = model.Where(p => p.EnrollNumber == item.EnrollNumber).OrderBy(p => p.AttendancedOn).FirstOrDefault();
                    dictionary.Add(item.Phone, late.AttendancedOn);
                }
            }
            Push push = new Push();
            push.MorningLate(dictionary);
        }



        /// <summary>
        /// 下午忘记打卡通知
        /// 18点-18点半
        /// </summary>
        public static void Afternoon()
        {
            var model = Repository.GetAttendanceCaches();
            var person = Repository.GetPerson();

            List<string> none = new List<string>();
            var now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, now.Day, 18, 0, 0);
            foreach (var item in person)
            {
                var exists = model.Exists(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn >= start && p.AttendancedOn <= now);
                if (!exists)
                {
                    none.Add(item.Phone);
                }
            }
            Push push = new Push();

            push.Afternoon(none);
        }

        /// <summary>
        /// 中午忘记打卡通知
        /// </summary>
        public static void Noon()
        {
            var model = Repository.GetAttendanceCaches();
            var person = Repository.GetPerson();

            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            var now = DateTime.Now;
            var start = new DateTime(now.Year, now.Month, now.Day, 12, 0, 0);
            foreach (var item in person)
            {
                var record = model.Where(p => p.EnrollNumber == item.EnrollNumber && p.AttendancedOn >= start && p.AttendancedOn <= now).ToList();
                if (record.Count == 0 || record.Count == 1)
                {
                    dictionary.Add(item.Phone, record.Count);
                }
                else
                {
                    var time = record.Last().AttendancedOn - record.First().AttendancedOn;
                    if (time.Minutes <= 5)
                    {
                        dictionary.Add(item.Phone, 1);
                    }
                }
            }

            Push push = new Push();
            push.Noon(dictionary);
        }
    }
}
