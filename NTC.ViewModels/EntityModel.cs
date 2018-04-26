using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class DashBoardEntityModel
    {
        public string HighestConductorPoints { get; set; }
        public string HighestDriverPoints { get; set; }
        public int RedNoticeDrivers { get; set; }
        public int RedNoticeConductors { get; set; }
        public int RedNoticeMembers { get; set; }
        public string HighestDriverComplain { get; set; }
        public string HighestConductorComplain { get; set; }
    }

    
    public class MemberEntityModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string TrainingCenter { get; set; }
        public string TrainingCertificateNo { get; set; }
        public int Points { get; set; }
        public int? Total { get; set; }
        public string NIC { get; set; }
        public string NTCNo { get; set; }
        public string Code { get; set; }
        //public DateTime? CreatedDate { get; set; }
    }

    public class MeritReportEntityModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime InqueryDate { get; set; }
        public string NTCNo { get; set; }


    }
}
