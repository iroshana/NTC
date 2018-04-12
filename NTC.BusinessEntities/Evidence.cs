using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Evidence
    {
        public Evidence()
        {
            Complains = new HashSet<Complain>();
        }
        public int ID { get; set; }
        public string EvidenceNo { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }

    }
}
