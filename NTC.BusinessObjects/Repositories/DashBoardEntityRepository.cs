using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class DashBoardEntityRepository:RepositoryBase<DashBoardEntityModel>, IDashBoardEntityRepository
    {
        public DashBoardEntityRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
