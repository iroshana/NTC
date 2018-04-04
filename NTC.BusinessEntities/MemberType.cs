using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class MemberType
    {
        public MemberType()
        {
            Members = new HashSet<Member>();
        }
        public int ID { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}
