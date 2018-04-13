using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class OfficerRepository : RepositoryBase<Officer>, IOfficerRepository
    {
        public OfficerRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
