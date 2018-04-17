using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class MeritReportRepository:RepositoryBase<MeritReportEntityModel>, IMeritReportRepository
    {
        public MeritReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
