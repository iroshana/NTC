using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
     public partial class Complain
    {
        public Complain()
        {
            ComplainCategories = new HashSet<ComplainCategory>();
        }
        public int ID { get; set; }
        public string ComplainNo { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }
        public string Place { get; set; }
        public DateTime Date { get; set; }
        public string Method { get; set; }
        public string ComplainCode { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public int? EvidenceId { get; set; }
        public int ConductorId { get; set; }
        public int DriverId { get; set; }
        public bool IsEvidenceHave { get; set; }
        public bool IsInqueryParticipation { get; set; }
        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DriverId")]
        public virtual Member Member { get; set; }
        [ForeignKey("EvidenceId")]
        public virtual Evidence Evidence { get; set; }
        public virtual ICollection<ComplainCategory> ComplainCategories { get; set; }
    }
}
