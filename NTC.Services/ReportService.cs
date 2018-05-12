using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.Services
{
    public class ReportService:EntityService<MeritReportEntityModel>, IReportService
    {
        #region Member Variables

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMeritReportRepository _meritReportRepository;

        #endregion Member Variables

        #region Constructor

        public ReportService(IUnitOfWork unitOfWork, IMeritReportRepository meritReportRepository)
            :base(unitOfWork, meritReportRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _meritReportRepository = meritReportRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<MeritReportEntityModel> CreateReport(int colorCode, DateTime? fromDate, DateTime? toDate, int typeId, string order)
        {
            try
            {
                string errorMessage = String.Empty;
                object[] param = {
                        new SqlParameter("@colorCode", colorCode),
                        new SqlParameter("@createdDateFrom", fromDate == null ? DateTime.Now.Date.AddMonths(-1).ToString(@"yyyy-MM-dd 00:00:00") : fromDate.Value.ToString(@"yyyy-MM-dd 00:00:00")),
                        new SqlParameter("@createdDateTo", toDate == null ? DateTime.Now.Date.ToString(@"yyyy-MM-dd 23:59:59") : toDate.Value.AddDays(1).ToString(@"yyyy-MM-dd 00:00:01")),
                        new SqlParameter("@typeId", typeId),
                        new SqlParameter("@orderBy",String.IsNullOrEmpty(order)? "ASC":order)
                };
                return _meritReportRepository.ExecuteStoredProcedure("dbo.DeMeritReports @colorCode, @createdDateFrom, @createdDateTo, @typeId, @orderBy", param).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        #endregion Constructor
    }
}
