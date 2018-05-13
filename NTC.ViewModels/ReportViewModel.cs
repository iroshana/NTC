using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class MeritReportViewModel
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string description { get; set; }
        public string inqueryDate { get; set; }
        public string ntcNo { get; set; }
        public int count { get; set; }

    }

    public class MeritReportDateModel
    {
        public int colorCodeId { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? toDate { get; set; }
        public int typeId { get; set; }
        public string order { get; set; }

    }
}
