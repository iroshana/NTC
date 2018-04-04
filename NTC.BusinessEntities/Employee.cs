using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Employee
    {
        public Employee()
        {
            DeMerits = new HashSet<DeMerit>();
            WorkerNotices = new HashSet<EmployeeNotice>();
        }
        public int ID { get; set; }
        public int UserID { get; set; }
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
        public virtual EmployeeType EmployeeType { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
        public virtual ICollection<EmployeeNotice> WorkerNotices { get; set; }
    }
}
