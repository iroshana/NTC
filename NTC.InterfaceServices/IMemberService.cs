using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IMemberService:IEntityService<Member>
    {
        Member GetEmployee(int Id);
        void Add(Member member, out string erroeMessage);
    }
}
