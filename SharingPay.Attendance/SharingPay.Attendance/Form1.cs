using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using SharingPay.Attendance.Data;
using SharingPay.Attendance.Model;
using SharingPay.Attendance.Rule;

namespace SharingPay.Attendance
{
    public partial class Form1 : Form
    {
        private int m_nCurSelID = 1;
        /// <summary>
        /// 是否连接打卡机
        /// </summary>
        bool opened = false;
        private readonly string connectionString = "Server=47.94.199.92;Port=3306;Database=sharingpay_attendance;Uid=root;password=AdmiN@2017q;Charset=utf8;";

        public Form1()
        {
            InitializeComponent();
            Close_Btn.Enabled = false;
        }

        System.Timers.Timer timer;


        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_Btn_Click(object sender, EventArgs e)
        {
            timer = new System.Timers.Timer();
            timer.Interval = 1000 * 30;
            timer.Elapsed += tick; ;
            timer.Start();
            Start_Btn.Enabled = false;
            Close_Btn.Enabled = true;
        }


        /// <summary>
        /// 结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Btn_Click(object sender, EventArgs e)
        {
            if (timer != null)
                timer.Close();

            Closed();
            Start_Btn.Enabled = true;
            Close_Btn.Enabled = false;
        }


        /// <summary>
        /// 定时轮询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                Run();
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
            }
            timer.Start();
        }

        private void Run()
        {

            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                Repository.UpdateTimerStatus();
            }

            var setting = Repository.GetTimerSetting();
            var now = new DateTime(2017, 7, 26, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            var set = setting.FirstOrDefault(p => p.Status == 100 && p.StartTimer <= now && p.EndTimer >= now);
            if (set == null)
                return;

            ReadCurrent();
            ReadCurrent(connectionString);

            switch (set.TimerId)
            {
                case 1:  //上午
                    AttendanceRule.Morning();
                    break;
                case 2:  //中午第一次
                    AttendanceRule.Noon();
                    break;
                case 3:  //中午第二次
                    AttendanceRule.Noon();
                    break;
                case 4:  //下午
                    AttendanceRule.Afternoon();
                    break;
                case 5:  //早上迟到提醒
                    AttendanceRule.MorningLate();
                    break;
                case 6:  //更新全部
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                    ReadAll(connectionString);
                    break;
            }

            Repository.UpdateTimerStatusById(set.TimerId);
        }

        private void Opend()
        {
            try
            {
                if (opened)
                {
                    return;
                }

                bool bRet;
                int nPort = 5005;
                int nPassword = 123456;
                string strIP = "192.168.1.208";
                bRet = axFP_CLOCK.SetIPAddress(ref strIP, nPort, nPassword);
                if (!bRet)
                {
                    MessageBox.Show("连接打卡机失败!");
                    return;
                }

                bRet = axFP_CLOCK.OpenCommPort(m_nCurSelID);
                if (bRet)
                {
                    opened = true;
                    Start_Btn.Text = "关闭";
                }
            }
            catch (Exception exception)
            {
                Log.Error("打开连接打卡机异常:" + exception.Message);
            }
        }

        private void Closed()
        {
            try
            {
                if (opened)
                {
                    Start_Btn.Text = "打开";
                    opened = false;

                    axFP_CLOCK.CloseCommPort();
                    return;
                }
            }
            catch (Exception exception)
            {
                Log.Error("关闭连接打卡机异常:" + exception.Message);
            }

        }

        /// <summary>
        /// 读取所有
        /// </summary>
        private List<GeneralLogInfo> Read()
        {
            List<GeneralLogInfo> myArray = new List<GeneralLogInfo>();
            try
            {
                Opend();
                bool bRet;
                GeneralLogInfo gLogInfo = new GeneralLogInfo();



                bRet = axFP_CLOCK.ReadAllGLogData(1);
                if (!bRet)
                {
                    axFP_CLOCK.EnableDevice(1, 1);
                    Log.Error("打卡记录读取失败!");
                    return myArray;
                }

                do
                {
                    bRet = axFP_CLOCK.GetAllGLogDataWithSecond(1,
                        ref gLogInfo.dwTMachineNumber,
                        ref gLogInfo.dwEnrollNumber,
                        ref gLogInfo.dwEMachineNumber,
                        ref gLogInfo.dwVerifyMode,
                        ref gLogInfo.dwYear,
                        ref gLogInfo.dwMonth,
                        ref gLogInfo.dwDay,
                        ref gLogInfo.dwHour,
                        ref gLogInfo.dwMinute,
                        ref gLogInfo.dwSecond
                        );

                    if (bRet)
                    {
                        myArray.Add(gLogInfo);
                    }

                } while (bRet);

                Closed();

                axFP_CLOCK.EnableDevice(1, 1);
            }
            catch (Exception exception)
            {
                Log.Error("打卡记录读取异常：" + exception.Message);
            }
            finally
            {
                Closed();
            }
            return myArray;
        }

        /// <summary>
        /// 读取当天
        /// </summary>
        public void ReadCurrent(string connectionString = "")
        {
            var myArray = Read();
            var now = DateTime.Now;
            List<AttendanceCach> attendance = new List<AttendanceCach>();

            Repository.RemoveCach(connectionString);

            foreach (GeneralLogInfo gInfo in myArray)
            {
                if (now.Year == gInfo.dwYear && now.Month == gInfo.dwMonth && now.Day == gInfo.dwDay)
                {
                    //如果要提高效率
                    DateTime dt = new DateTime(gInfo.dwYear,
                        gInfo.dwMonth,
                        gInfo.dwDay,
                        gInfo.dwHour,
                        gInfo.dwMinute,
                        gInfo.dwSecond);

                    attendance.Add(new AttendanceCach()
                    {
                        EnrollNumber = gInfo.dwEnrollNumber,
                        AttendancedOn = dt,
                        CreatedOn = DateTime.Now
                    });

                }
            }

            if (attendance.Count > 0)
            {
                Repository.InserCach(attendance, connectionString);
                attendance.Clear();
            }

        }

        /// <summary>
        /// 读取全部
        /// </summary>
        public void ReadAll(string connectionString = "")
        {
            try
            {
                Repository.Remove(connectionString);
                List<AttendanceRecord> attendance = new List<AttendanceRecord>();
                var myArray = Read();
                foreach (GeneralLogInfo gInfo in myArray)
                {
                    //如果要提高效率
                    DateTime dt = new DateTime(gInfo.dwYear,
                        gInfo.dwMonth,
                        gInfo.dwDay,
                        gInfo.dwHour,
                        gInfo.dwMinute,
                        gInfo.dwSecond);

                    attendance.Add(new AttendanceRecord()
                    {
                        EnrollNumber = gInfo.dwEnrollNumber,
                        AttendancedOn = dt,
                        CreatedOn = DateTime.Now
                    });
                }

                if (attendance.Any())
                {

                    Repository.Inser(attendance, connectionString);
                    attendance.Clear();
                }
            }
            catch (Exception exception)
            {
                Log.Error("读取全部异常：" + exception.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ReadAll();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void notifyIconSystem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.notifyIconSystem.Visible = false;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)//最小化　　　　　 
            {
                this.ShowInTaskbar = false;
                this.notifyIconSystem.Visible = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.notifyIconSystem.Visible = false;
        }

        private void Open_Click(object sender, EventArgs e)
        {

            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.ShowInTaskbar = false;
            this.notifyIconSystem.Visible = true;
        }
    }
}
