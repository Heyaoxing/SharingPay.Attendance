using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace SharingPay.Attendance.Data
{
    public class Repository
    {
        /// <summary>
        /// 移除缓存记录
        /// </summary>
        public static void RemoveCach(string connectionString = "")
        {
            using (var conn = DapperFactory.CrateOracleConnection(connectionString))
            {
                conn.Execute("delete from AttendanceCach;");
            }
        }

        /// <summary>
        /// 移除历史记录
        /// </summary>
        public static void Remove(string connectionString = "")
        {
            using (var conn = DapperFactory.CrateOracleConnection(connectionString))
            {
                conn.Execute("delete from attendancerecord;");
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="model"></param>
        public static void Inser(List<AttendanceRecord> model, string connectionString = "")
        {
            using (var conn = DapperFactory.CrateOracleConnection(connectionString))
            {
                conn.Execute(@"INSERT INTO attendancerecord (
                                    EnrollNumber,
                                    AttendancedOn,
                                    CreatedOn
                                )
                                VALUES
                                    (
                                        @EnrollNumber,
                                        @AttendancedOn,
                                        @CreatedOn
                                    ); ", model);
            }
        }


        /// <summary>
        /// 批量插入缓存表
        /// </summary>
        /// <param name="model"></param>
        public static void InserCach(List<AttendanceCach> model, string connectionString = "")
        {
            using (var conn = DapperFactory.CrateOracleConnection(connectionString))
            {
                conn.Execute(@"INSERT INTO AttendanceCach (
                                    EnrollNumber,
                                    AttendancedOn,
                                    CreatedOn
                                )
                                VALUES
                                    (
                                        @EnrollNumber,
                                        @AttendancedOn,
                                        @CreatedOn
                                    ); ", model);
            }
        }

        /// <summary>
        /// 获取短信通知人员集合
        /// </summary>
        /// <returns></returns>
        public static List<PersonSetting> GetPerson()
        {
            List<PersonSetting> person = new List<PersonSetting>();
            using (var conn = DapperFactory.CrateOracleConnection())
            {
                person = conn.Query<PersonSetting>("select * from PersonSetting;").ToList();
            }
            return person;
        }

        /// <summary>
        /// 获取缓存记录
        /// </summary>
        /// <returns></returns>
        public static List<AttendanceCach> GetAttendanceCaches()
        {
            List<AttendanceCach> cach = new List<AttendanceCach>();
            using (var conn = DapperFactory.CrateOracleConnection())
            {
                cach = conn.Query<AttendanceCach>("select * from AttendanceCach;").ToList();
            }
            return cach;
        }

        /// <summary>
        /// 获取时间配置
        /// </summary>
        /// <returns></returns>
        public static List<TimerSetting> GetTimerSetting()
        {
            List<TimerSetting> timer = new List<TimerSetting>();
            using (var conn = DapperFactory.CrateOracleConnection())
            {
                timer = conn.Query<TimerSetting>("select * from TimerSetting;").ToList();
            }

            return timer;
        }

        /// <summary>
        ///更新完成状态
        /// </summary>
        /// <param name="timerId"></param>
        public static void UpdateTimerStatusById(int timerId)
        {
            using (var conn = DapperFactory.CrateOracleConnection())
            {
                conn.Execute("update TimerSetting set Status=200,UpdatedOn=now() where TimerId=@TimerId", new { TimerId = timerId });
            }
        }

        /// <summary>
        /// 重置计划
        /// </summary>
        public static void UpdateTimerStatus()
        {
            using (var conn = DapperFactory.CrateOracleConnection())
            {
                conn.Execute("update TimerSetting set Status=100,UpdatedOn=now();");
            }
        }
    }
}
