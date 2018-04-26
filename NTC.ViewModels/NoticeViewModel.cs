using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class NoticeViewModel
    {
        public int ID { get; set; }
        public string NoticeCode { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public int memberId { get; set; }
        public bool IsSent { get; set; }
        public bool isGeneralNotice { get; set; }
    }

    public class BulkNoticeViewModel
    {
        public int ID { get; set; }
        public string NoticeCode { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public List<int> members { get; set; }
    }

    public class MemberNoticeViewModel
    {
        public int ID { get; set; }
        public int MemberId { get; set; }
        public bool IsSent { get; set; }
        public bool IsOpened { get; set; }
    }
}
