using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IReportService:IEntityService<MeritReportEntityModel>
    {
        IEnumerable<MeritReportEntityModel> CreateReport(int colorCode,DateTime? fromDate, DateTime? toDate,int typeId,string order);
    }
}
