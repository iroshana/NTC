using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class LogmessageRepository : RepositoryBase<LogMessage>, ILogmessageRepository
    {
        public LogmessageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
