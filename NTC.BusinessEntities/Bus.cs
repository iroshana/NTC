using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Bus
    {
        public Bus()
        {
            Complains = new HashSet<Complain>();
            DeMerits = new HashSet<DeMerit>();
        }
        public int ID { get; set; }
        public string LicenceNo { get; set; }
        public string Type { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
    }
}
