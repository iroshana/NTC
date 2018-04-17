using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class DashBoardEntityModel
    {
        public int HighestConductorPoints { get; set; }
        public int HighestDriverPoints { get; set; }
        public int RedNoticeDrivers { get; set; }
        public int RedNoticeConductors { get; set; }
        public int RedNoticeMembers { get; set; }
        public int HighestDriverComplain { get; set; }
        public int HighestConductorComplain { get; set; }
    }

    
    public class MemberEntityModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string TrainingCenter { get; set; }
        public string TrainingCertificateNo { get; set; }
        public int Points { get; set; }
        public string NIC { get; set; }
        public string NTCNo { get; set; }
        public string Code { get; set; }
        
    }

    public class MeritReportEntityModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public DateTime InqueryDate { get; set; }

    }
}
