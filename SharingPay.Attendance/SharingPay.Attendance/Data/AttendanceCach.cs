using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharingPay.Attendance.Data
{
    public class AttendanceCach
    {
        public long CachId { set; get; }
        public int EnrollNumber { set; get; }
        public DateTime AttendancedOn { set; get; }
        public DateTime CreatedOn { set; get; }
    }
}
