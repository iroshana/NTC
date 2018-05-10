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
    
    public class DashBoardController : ApiController
    {
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IMemberService _member;
        private readonly IDeMeritService _demerit;
        private readonly IComplainService _complain;
        public DashBoardController(IEventLogService eventLog, ICommonDataService common, IMemberService member, IDeMeritService demerit, IComplainService complain)
        {
            _eventLog = eventLog;
            _common = common;
            _member = member;
            _demerit = demerit;
            _complain = complain;
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

                int BestDriverMonth = 0;
                int BestDriverYear = 0;
                int BestConductorMonth = 0;
                int BestConductorYear = 0;


                List<Member> membesDateList = new List<Member>();
                IEnumerable<Member> memberList = new List<Member>();
                memberList = _member.GetAll().ToList();

                foreach (Member mem in memberList)
                {
                    if (mem.MemberType.Code == "Driver")
                    {
                        int point = 0;
                        IEnumerable<DeMerit> memberDe = _demerit.GetAll(x => x.MemberId == mem.ID).ToList();
                        memberDe = memberDe.Where(x => x.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && x.CreatedDate.Date <= DateTime.Now.Date).ToList();
                        foreach (DeMerit mer in memberDe)
                        {
                            point += mer.MemberDeMerits.Sum(x => x.Point);
                            if (point < 2)
                            {
                                if(_complain.GetAll(x=>x.DriverId == mem.ID).Count() < 1)
                                {
                                    BestDriverMonth++;
                                }
                            }
                        }
                        if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 1)
                            {
                                BestDriverMonth++;
                            }
                        }
                    }
                    else
                    {
                        int point = 0;
                        IEnumerable<DeMerit> memberDe = _demerit.GetAll(x => x.MemberId == mem.ID).ToList();
                        memberDe = memberDe.Where(x => x.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && x.CreatedDate.Date <= DateTime.Now.Date).ToList();
                        foreach (DeMerit mer in memberDe)
                        {
                            point += mer.MemberDeMerits.Sum(x => x.Point);
                            if (point < 2)
                            {
                                if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 1)
                                {
                                    BestConductorMonth++;
                                }
                            }
                        }
                        if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 1)
                            {
                                BestConductorMonth++;
                            }
                        }
                    }
                }
                foreach (Member mem in memberList)
                {
                    if (mem.MemberType.Code == "Driver")
                    {
                        int point = 0;
                        IEnumerable<DeMerit> memberDe = _demerit.GetAll(x => x.MemberId == mem.ID).ToList();
                        memberDe = memberDe.Where(x => x.CreatedDate.Date >= DateTime.Now.Date.AddYears(-1) && x.CreatedDate.Date <= DateTime.Now.Date).ToList();
                        foreach (DeMerit mer in memberDe)
                        {
                            point += mer.MemberDeMerits.Sum(x => x.Point);
                            if (point < 2)
                            {
                                if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 1)
                                {
                                    BestDriverYear++;
                                }
                            }
                        }
                        if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 1)
                            {
                                BestDriverYear++;
                            }
                        }
                    }
                    else
                    {
                        int point = 0;
                        IEnumerable<DeMerit> memberDe = _demerit.GetAll(x => x.MemberId == mem.ID).ToList();
                        memberDe = memberDe.Where(x => x.CreatedDate.Date >= DateTime.Now.Date.AddYears(-1) && x.CreatedDate.Date <= DateTime.Now.Date).ToList();
                        foreach (DeMerit mer in memberDe)
                        {
                            point += mer.MemberDeMerits.Sum(x => x.Point);
                            if (point < 2)
                            {
                                if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 1)
                                {
                                    BestConductorYear++;
                                }
                            }
                        }
                        if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 1)
                            {
                                BestConductorYear++;
                            }
                        }
                    }
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
                    dashboardView.bestdriversofYear = BestDriverYear;
                    dashboardView.bestConductorsofYear = BestConductorYear;
                    dashboardView.bestdriversofMonth = BestDriverMonth;
                    dashboardView.bestConductorsofMonth = BestConductorMonth;
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
