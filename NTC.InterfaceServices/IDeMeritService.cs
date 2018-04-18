using NTC.BusinessEntities;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IDeMeritService:IEntityService<DeMerit>
    {
        void Add(DeMerit deMerit, out string errorMessage);
        DeMerit GetDeMeritByNo(int deMeritId);
        IEnumerable<DeMerit> GetDeMeritByUser(int memberId);
        IEnumerable<DeMerit> GetAllDeMerits();
        DeMerit GetLastDeMeritNo();
        IList<MemberDeMeritViewModel> GetDeMeritSummery(int memberId);
    }
}
