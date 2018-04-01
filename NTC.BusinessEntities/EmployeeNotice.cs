using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class EmployeeNotice
    {
        public int ID { get; set; }
        public int EmployeeId { get; set; }
        public int NoticeId { get; set; }
        public bool IsSent { get; set; }
        public bool  IsOpened { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("NoticeId")]
        public virtual Notice Notice { get; set; }

    }
}
