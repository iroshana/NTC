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

        public DashBoardController(IEventLogService eventLog, ICommonDataService common)
        {
            _eventLog = eventLog;
            _common = common;
        }
        #region GetDashboardCounts
        [HttpGet]
        public IHttpActionResult GetDashboardCounts()
        {
            try
            {
                List<DashBoardViewModel> dashboardCounts = new List<DashBoardViewModel>();
                IEnumerable<DashBoardEntityModel> dashboards = new List<DashBoardEntityModel>();
                dashboards = _common.GetDashBoardCounts();
                if (dashboards != null)
                {
                    foreach (DashBoardEntityModel dashboard in dashboards)
                    {
                        DashBoardViewModel dashboardView = new DashBoardViewModel();
                        dashboardView.highestConductorComplain = dashboard.HighestConductorComplain;
                        dashboardView.highestDriverComplain = dashboard.HighestDriverComplain;
                        dashboardView.highestConductorPoints = dashboard.HighestConductorPoints;
                        dashboardView.highestDriverPoints = dashboard.HighestDriverPoints;
                        dashboardView.redNoticeConductors = dashboard.RedNoticeConductors;
                        dashboardView.redNoticeDrivers = dashboard.RedNoticeDrivers;
                        dashboardView.redNoticeMembers = dashboard.RedNoticeMembers;
                        dashboardCounts.Add(dashboardView);
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { dashboard = dashboardCounts, messageCode = messageData };
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
