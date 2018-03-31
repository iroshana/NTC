
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Conductor
    {
        public Conductor()
        {
            DeMerits = new HashSet<DeMerit>();
            WorkerNotices = new HashSet<WorkerNotice>();
        }
        public int ID { get; set; }
        public int UserID { get; set; }
        public string TrainingCertificateNo { get; set; }
        public string HighestEducation { get; set; }
        public DateTime JoinDate { get; set; }
        public string IssuedDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public string ImagePath { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
        public virtual ICollection<WorkerNotice> WorkerNotices { get; set; }
    }
}
