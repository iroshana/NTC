using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Notice
    {
        public Notice()
        {
            MemberNotices = new HashSet<MemberNotice>();
        }
        public int ID { get; set; }
        public string NoticeCode { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSent { get; set; }
        public virtual ICollection<MemberNotice> MemberNotices { get; set; }

    }
}
