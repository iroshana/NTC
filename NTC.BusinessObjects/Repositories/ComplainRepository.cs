using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class ComplainRepository : RepositoryBase<Complain>, IComplainRepository
    {
        public ComplainRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
