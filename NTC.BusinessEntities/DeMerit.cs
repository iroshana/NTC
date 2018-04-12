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
        public DeMerit()
        {
            MemberDeMerits = new HashSet<MemberDeMerit>();
        }
        public int ID { get; set; }
        public string DeMeritNo { get; set; }
        public int MemberId { get; set; }
        public int RouteId { get; set; }
        public DateTime InqueryDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OfficeriId { get; set; }
        public int BusId { get; set; }
        [ForeignKey("BusId")]
        public virtual Bus Bus { get; set; }
        [ForeignKey("RouteId")]
        public virtual Route Route { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }
        [ForeignKey("OfficeriId")]
        public virtual Officer Officer { get; set; }
        public virtual ICollection<MemberDeMerit> MemberDeMerits { get; set; }


    }
}
