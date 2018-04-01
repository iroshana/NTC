using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class DeMerit
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int EmployeeId { get; set; }
        public int MeritId { get; set; }
        public int RouteId { get; set; }
        public DateTime InqueryDate { get; set; }
        public int OfficeriId { get; set; }
        public int BusId { get; set; }
        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
        [ForeignKey("OfficeriId")]
        public virtual Officer Officer { get; set; }
        [ForeignKey("MeritId")]
        public virtual Merit Merit { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

    }
}
