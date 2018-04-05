using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class MemberTypeRepository:RepositoryBase<MemberType>,IMemberTypeRepository
    {
        public MemberTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
