using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface INoticeService:IEntityService<Notice>
    {
        void Add(Notice notice, out string errorMessage);
        IEnumerable<Notice> GetAllNotices();
        void UpdaterNotice(Notice notice, out string errorMessage);
    }
}
