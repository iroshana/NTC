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
    
    public class DashBoardController : ApiController
    {
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IMemberService _member;

        public DashBoardController(IEventLogService eventLog, ICommonDataService common, IMemberService member)
        {
            _eventLog = eventLog;
            _common = common;
            _member = member;
        }
        #region GetDashboardCounts
        [HttpGet]
        public IHttpActionResult GetDashboardCounts()
        {
            try
            {
                DashBoardViewModel dashboardView = new DashBoardViewModel();
                DashBoardEntityModel dashboard = new DashBoardEntityModel();
                
                dashboard = _common.GetDashBoardCounts();
                
                if (dashboard != null)
                {
                    
                    dashboardView.highestConductorComplain = dashboard.HighestConductorComplain;
                    dashboardView.highestDriverComplain = dashboard.HighestDriverComplain;
                    dashboardView.highestConductorPoints = dashboard.HighestConductorPoints;
                    dashboardView.highestDriverPoints = dashboard.HighestDriverPoints;
                    dashboardView.redNoticeConductors = dashboard.RedNoticeConductors;
                    dashboardView.redNoticeDrivers = dashboard.RedNoticeDrivers;
                    dashboardView.redNoticeMembers = dashboard.RedNoticeMembers;
                    dashboardView.bestdriversofYear = dashboard.BestDriverYear;
                    dashboardView.bestConductorsofYear = dashboard.BestConductorYear;
                    dashboardView.bestdriversofMonth = dashboard.BestDriverMonth;
                    dashboardView.bestConductorsofMonth = dashboard.BestConductorMonth;
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { dashboard = dashboardView, messageCode = messageData };
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
