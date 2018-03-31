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
            Categories = new HashSet<Category>();
        }
        public int ID { get; set; }
        public string ComplainNo { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }
        public string Place { get; set; }
        public string Time { get; set; }
        public string Method { get; set; }
        public string ComplainCode { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int? EvidenceId { get; set; }
        public int? DriverId { get; set; }
        public int? ConductorId { get; set; }
        public bool IsEvidenceHave { get; set; }
        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
        [ForeignKey("ConductorId")]
        public virtual Conductor Conductor { get; set; }
        [ForeignKey("EvidenceId")]
        public virtual Evidence Evidence { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
