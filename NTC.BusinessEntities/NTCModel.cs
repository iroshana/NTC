using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class NTCModel: DbContext
    {
        public NTCModel()
            : base("name=NTCEntities")
        {
        }
    }
}
