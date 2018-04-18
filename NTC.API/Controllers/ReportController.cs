using NTC.BusinessEntities;
using NTC.InterfaceServices;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace NTC.API.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IReportService _report;
        private readonly IComplainService _complain;
        public ReportController(IEventLogService eventLog, ICommonDataService common, IReportService report,IComplainService complain)
        {
            _eventLog = eventLog;
            _common = common;
            _report = report;
            _complain = complain;
        }

        #region GetDemeritReports
        [HttpPost]
        public IHttpActionResult GetDemeritReports(MeritReportDateModel search)
        {
            try
            {
                List<MeritReportViewModel> meritList = new List<MeritReportViewModel>();
                IEnumerable<MeritReportEntityModel> merits = new List<MeritReportEntityModel>();
                merits = _report.CreateReport(search.colorCodeId, search.fromDate, search.toDate, search.typeId, search.order);

                if (merits.Count() > 0)
                {
                    foreach (MeritReportEntityModel merit in merits)
                    {
                        MeritReportViewModel meritView = new MeritReportViewModel();
                        meritView.id = merit.ID;
                        meritView.fullName = merit.FullName;
                        meritView.description = merit.Description;
                        meritView.inqueryDate = merit.InqueryDate.ToString(@"yyyy-MM-dd");
                        meritView.ntcNo = merit.NTCNo;
                        meritList.Add(meritView);
                    }
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { roleList = meritList, messageCode = messageData };
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                string errorLogId = _eventLog.WriteLogs(User.Identity.Name, ex, MethodBase.GetCurrentMethod().Name);
                var messageData = new { code = Constant.ErrorMessageCode, message = String.Format(Constant.MessageTaskmateError, errorLogId) };
                var returnObject = new { messageCode = messageData };
                return Ok(returnObject);
            }
        }
        #endregion

        #region GetComplainReport
        [HttpGet]
        public IHttpActionResult GetComplainReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<ComplainReportViewModel> complainList = new List<ComplainReportViewModel>();
                IEnumerable<Complain> complains = new List<Complain>();
                complains = _complain.GetAllComplains(fromDate,toDate) ;

                if (complains.Count() > 0)
                {
                    foreach (Complain complain in complains)
                    {
                        ComplainReportViewModel complainView = new ComplainReportViewModel();
                        complainView.driverName = complain.Member.FullName;
                        complainView.ntcNo = complain.Member.NTCNo;
                        complainView.complain = String.IsNullOrEmpty(complain.Description)?String.Empty:complain.Description;
                        complainView.complainStatus = complain.ComplainStatus;

                        complainList.Add(complainView);
                    }
                }
                
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainList = complainList, messageCode = messageData };
                return Ok(returnObject);
            }
            catch (Exception ex)
            {
                string errorLogId = _eventLog.WriteLogs(User.Identity.Name, ex, MethodBase.GetCurrentMethod().Name);
                var messageData = new { code = Constant.ErrorMessageCode, message = String.Format(Constant.MessageTaskmateError, errorLogId) };
                var returnObject = new { messageCode = messageData };
                return Ok(returnObject);
            }
        }
        #endregion


    }
}
