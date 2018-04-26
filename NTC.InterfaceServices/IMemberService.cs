using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTC.ViewModels;

namespace NTC.InterfaceServices
{
    public interface IMemberService:IEntityService<Member>
    {
        Member GetMember(int Id);
        IEnumerable<Member> GetAllMembers();
        IEnumerable<MemberEntityModel> GetAllMembersSP(int colorCode,DateTime? fromDate,DateTime? toDate,int type);
        void Add(Member member, out string erroeMessage);
        Member GetBestMember(DateTime date,bool isMonth);
    }
}
