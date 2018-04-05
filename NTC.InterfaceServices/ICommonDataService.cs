using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface ICommonDataService
    {
        IEnumerable<MemberType> GetAllMemberTypes();
        IEnumerable<Bus> SearchBuses(string busNo);
        Bus GetBusByNo(string busNo);
        IEnumerable<Category> GetAllCategories();
    }
}
