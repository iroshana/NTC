using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [StringLength(100)]
        public string ShortName { get; set; }
        public DateTime DOB { get; set; }
        [StringLength(1000)]
        public string PermanetAddress { get; set; }
        [StringLength(1000)]
        public string CurrentAddress { get; set; }
        public string TelNo { get; set; }
        public string NIC { get; set; }
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
        public string NTCNo { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        [ForeignKey("TypeId")]
        public virtual MemberType MemberType { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
        public virtual ICollection<MemberNotice> WorkerNotices { get; set; }
    }
}
