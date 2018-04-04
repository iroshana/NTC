using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class MemberDeMerit
    {
        public int ID { get; set; }
        public int DeMeritId { get; set; }
        public int MeritId { get; set; }
        public int Point { get; set; }
        [ForeignKey("MeritId")]
        public virtual Merit Merit { get; set; }
        [ForeignKey("DeMeritId")]
        public virtual DeMerit DeMerit { get; set; }
    }
}
