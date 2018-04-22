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

                int bestdriversofMonth = 0;
                int bestConductorsofMonth = 0;
                int bestdriversofYear = 0;
                int bestConductorsofYear = 0;


                dashboard = _common.GetDashBoardCounts();

                IEnumerable<MemberEntityModel> drivers = new List<MemberEntityModel>();
                drivers = _member.GetAllMembersSP(0, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date, 1);
                if (drivers != null)
                {
                    bestdriversofMonth = drivers.Where(x => x.Total == null || x.Total.Value <= 2).ToList().Count();
                }

                IEnumerable<MemberEntityModel> conductors = new List<MemberEntityModel>();
                conductors = _member.GetAllMembersSP(0, DateTime.Now.Date.AddMonths(-1), DateTime.Now.Date, 2);
                if (conductors != null)
                {
                    bestConductorsofMonth = conductors.Where(x => x.Total == null || x.Total.Value <= 2).ToList().Count();
                }

                IEnumerable<MemberEntityModel> drivers1 = new List<MemberEntityModel>();
                drivers1 = _member.GetAllMembersSP(0, DateTime.Now.Date.AddYears(-1), DateTime.Now.Date, 1);
                if (drivers1 != null)
                {
                    bestdriversofYear = drivers1.Where(x => x.Total == null || x.Total.Value <= 2).ToList().Count();
                }

                IEnumerable<MemberEntityModel> conductors1 = new List<MemberEntityModel>();
                conductors1 = _member.GetAllMembersSP(0, DateTime.Now.Date.AddYears(-1), DateTime.Now.Date, 2);
                if (conductors1 != null)
                {
                    bestConductorsofYear = conductors1.Where(x => x.Total == null || x.Total.Value <= 2).ToList().Count();
                }

                if (dashboard != null)
                {
                    
                    dashboardView.highestConductorComplain = dashboard.HighestConductorComplain;
                    dashboardView.highestDriverComplain = dashboard.HighestDriverComplain;
                    dashboardView.highestConductorPoints = dashboard.HighestConductorPoints;
                    dashboardView.highestDriverPoints = dashboard.HighestDriverPoints;
                    dashboardView.redNoticeConductors = dashboard.RedNoticeConductors;
                    dashboardView.redNoticeDrivers = dashboard.RedNoticeDrivers;
                    dashboardView.redNoticeMembers = dashboard.RedNoticeMembers;
                    dashboardView.bestdriversofYear = bestdriversofYear;
                    dashboardView.bestConductorsofYear = bestConductorsofYear;
                    dashboardView.bestdriversofMonth = bestdriversofMonth;
                    dashboardView.bestConductorsofMonth = bestConductorsofMonth;
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
