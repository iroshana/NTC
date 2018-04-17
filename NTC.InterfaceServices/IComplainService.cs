using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IComplainService:IEntityService<Complain>
    {
        void Add(Complain complain,out string errorMessage);
        IEnumerable<Complain> GetComplainNo(int userId);
        Complain GetComplainByNo(string complainNo, int userId);
        Complain GetLastComplain();
        IEnumerable<Complain> GetAllComplains(DateTime fromDate,DateTime toDate);
    }
}
