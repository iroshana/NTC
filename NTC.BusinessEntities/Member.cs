using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Member
    {
        public Member()
        {
            DeMerits = new HashSet<DeMerit>();
            WorkerNotices = new HashSet<MemberNotice>();
        }
        public int ID { get; set; }
        public int? UserID { get; set; }
        public int TypeId { get; set; }
        public string TrainingCertificateNo { get; set; }
        public string LicenceNo { get; set; }
        public string TrainingCenter { get; set; }
        public string HighestEducation { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("TypeId")]
        public virtual MemberType EmployeeType { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
        public virtual ICollection<MemberNotice> WorkerNotices { get; set; }
    }
}
