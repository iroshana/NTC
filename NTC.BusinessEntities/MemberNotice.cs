using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class MemberNotice
    {
        public int ID { get; set; }
        public int MemberId { get; set; }
        public int NoticeId { get; set; }
        public bool IsSent { get; set; }
        public bool  IsOpened { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
        [ForeignKey("NoticeId")]
        public virtual Notice Notice { get; set; }

    }
}
