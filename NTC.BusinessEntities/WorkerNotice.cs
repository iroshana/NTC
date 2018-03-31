using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class WorkerNotice
    {
        public int ID { get; set; }
        public int? DriverId { get; set; }
        public int? ConductorId { get; set; }
        public int NoticeId { get; set; }
        public bool IsSent { get; set; }
        public bool  IsOpened { get; set; }
        [ForeignKey("DriverId")]
        public virtual Driver Driver { get; set; }
        [ForeignKey("ConductorId")]
        public virtual Conductor Conductor { get; set; }
        [ForeignKey("NoticeId")]
        public virtual Notice Notice { get; set; }

    }
}
