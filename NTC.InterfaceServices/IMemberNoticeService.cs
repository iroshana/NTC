using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IMemberNoticeService: IEntityService<MemberNotice>
    {
        IEnumerable<MemberNotice> GetAllMemeberNotice(int memberId, bool isSent, bool isOpen);
        void UpdateMemberNotice(MemberNotice notice, out string errorMessage);
    }
}
