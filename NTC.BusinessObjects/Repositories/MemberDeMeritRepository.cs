using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class MemberDeMeritRepository:RepositoryBase<MemberDeMerit>, IMemberDeMeritRepository
    {
        public MemberDeMeritRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
