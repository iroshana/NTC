
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Officer
    {
        public Officer()
        {
            DeMerits = new HashSet<DeMerit>();
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string TelNo { get; set; }
        public string NIC { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
    }
}
