using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class NoticeRepository:RepositoryBase<Notice>,INoticeRepository
    {
        public NoticeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
