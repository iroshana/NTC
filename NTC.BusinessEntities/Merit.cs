using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Merit
    {
        public Merit()
        {
            DeMerits = new HashSet<DeMerit>();
        }
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public int Point { get; set; }
        public int ColorCodeId { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }

    }
}
