using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Notice
    {
        public Notice()
        {
            WorkerNotices = new HashSet<EmployeeNotice>();
        }
        public int ID { get; set; }
        public string NoticeCode { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public virtual ICollection<EmployeeNotice> WorkerNotices { get; set; }

    }
}
