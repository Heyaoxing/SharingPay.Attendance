using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharingPay.Attendance.Data
{
    public class TimerSetting
    {
        public int TimerId { set; get; }
        public DateTime StartTimer { set; get; }
        public DateTime EndTimer { set; get; }
        public int Status { set; get; }
        public DateTime UpdatedOn { set; get; }
    }
}
