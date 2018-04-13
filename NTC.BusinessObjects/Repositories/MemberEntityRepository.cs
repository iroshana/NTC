using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class MemberEntityRepository:RepositoryBase<MemberEntityModel>, IMemberEntityRepository
    {
        public MemberEntityRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
