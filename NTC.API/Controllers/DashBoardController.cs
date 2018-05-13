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

                //dashboard = _common.GetDashBoardCounts();

                int BestDriverMonth = 0;
                int BestDriverYear = 0;
                int BestConductorMonth = 0;
                int BestConductorYear = 0;
                int rednoticeDriver = 0;
                int rednoticeCon = 0;
                int highestDriverComplain = 0;
                int highestConductorComplain = 0;
                int HighestConductorPoints = 0;
                int HighestDriverPoints = 0;


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
                        if (memberDe.Count() < 2)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 2)
                            {
                                BestDriverMonth++;
                            }
                        }
                        else if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 2)
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
                        if (memberDe.Count() < 2)
                        {
                            if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 2)
                            {
                                BestConductorMonth++;
                            }
                        }
                        else if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 2)
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
                        if (memberDe.Count() < 2)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 2)
                            {
                                BestDriverYear++;
                            }
                        }
                        else if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID).Count() < 2)
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
                        if (memberDe.Count() < 2)
                        {
                            if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 2)
                            {
                                BestConductorYear++;
                            }
                        }
                        else if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.ConductorId == mem.ID).Count() < 2)
                            {
                                BestConductorYear++;
                            }
                        }
                    }
                }


                IEnumerable<DeMerit> memberDem = _demerit.GetAll().ToList();
                memberDem = memberDem.Where(x => x.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && x.CreatedDate.Date <= DateTime.Now.Date).ToList();
                List<MeritDashBoardView> meritIdlistDriver = new List<MeritDashBoardView>();
                List<MeritDashBoardView> meritIdlistCond = new List<MeritDashBoardView>();
                foreach (DeMerit de in memberDem)
                {

                    if (de.Member.MemberType.Code == "Driver")
                    {
                        IEnumerable<MemberDeMerit> memDe = de.MemberDeMerits.Where(x => x.Merit.ColorCodeId == 1).ToList();
                        foreach (MemberDeMerit memdem in memDe.Where(z => z.Point != 0))
                        {
                            var a = meritIdlistDriver.Find(x => x.id == memdem.Merit.ID && x.memberId == de.MemberId);
                            if (a == null)
                            {
                                MeritDashBoardView md = new MeritDashBoardView();
                                md.id = memdem.Merit.ID;
                                md.point += memdem.Point;
                                md.memberId = de.MemberId;
                                meritIdlistDriver.Add(md);
                            }
                            else
                            {
                                a.point += memdem.Point;
                            }
                        }
                    }
                    else
                    {
                        IEnumerable<MemberDeMerit> memDe = de.MemberDeMerits.Where(x => x.Merit.ColorCodeId == 1).ToList();
                        foreach (MemberDeMerit memdem in memDe.Where(z => z.Point != 0))
                        {
                            var a = meritIdlistCond.Find(x => x.id == memdem.Merit.ID && x.memberId == de.MemberId);
                            if (a == null)
                            {
                                MeritDashBoardView md = new MeritDashBoardView();
                                md.id = memdem.Merit.ID;
                                md.point += memdem.Point;
                                md.memberId = de.MemberId;
                                meritIdlistCond.Add(md);
                            }
                            else
                            {
                                a.point += memdem.Point;
                            }
                        }
                    }
                }

                rednoticeDriver = meritIdlistDriver.Count() <= 0 ? 0 : meritIdlistDriver.Where(x => x.point > 2).GroupBy(z => z.memberId).Count();
                rednoticeCon = meritIdlistCond.Count() <= 0 ? 0 : meritIdlistCond.Where(x => x.point > 2).GroupBy(z => z.memberId).Count();


                IEnumerable<Complain> driverComplain = _complain.GetAll(x => x.Member.MemberType.Code == "Driver" && x.ComplainStatus != "Resolve");
                IEnumerable<Complain> conductComplain = _complain.GetAll(x => x.Member1.MemberType.Code == "Conductor" && x.ComplainStatus != "Resolve");
                List<MeritDashBoardView> highDriver = new List<MeritDashBoardView>();
                List<MeritDashBoardView> highCond = new List<MeritDashBoardView>();

                foreach (Complain dcomp in driverComplain)
                {
                    var a = highDriver.Find(x => x.memberId == dcomp.DriverId);
                    if (a == null && dcomp.DriverId != null)
                    {
                        MeritDashBoardView md = new MeritDashBoardView();
                        md.point = 1;
                        md.memberId = dcomp.DriverId.Value;
                        highDriver.Add(md);

                    }
                    else if(dcomp.DriverId != null)
                    {
                        a.point++;
                    }
                }

                foreach (Complain dcomp in conductComplain)
                {
                    var a = highCond.Find(x => x.memberId == dcomp.ConductorId);
                    if (a == null && dcomp.ConductorId != null)
                    {
                        MeritDashBoardView md = new MeritDashBoardView();
                        md.point = 1;
                        md.memberId = dcomp.ConductorId.Value;
                        highCond.Add(md);
                    }
                    else if(dcomp.ConductorId != null)
                    {
                        a.point++;
                    }
                }

                highDriver = highDriver.Where(w => w.point > 5).ToList();
                highCond = highCond.Where(w => w.point > 5).ToList();

                highestDriverComplain = highDriver.Count();
                highestConductorComplain = highCond.Count();

                IEnumerable<DeMerit> deMeritCond = _demerit.GetAll(z => z.Member.MemberType.Code == "Conductor").ToList();
                deMeritCond = deMeritCond.Where(c => c.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && c.CreatedDate <= DateTime.Now.Date).ToList();

                List<MeritDashBoardView> deMListCond = new List<MeritDashBoardView>();
                List<MeritDashBoardView> deMListDri = new List<MeritDashBoardView>();
                foreach (DeMerit deme in deMeritCond)
                {
                    var a = deMListCond.Find(x => x.memberId == deme.MemberId);
                    if (a == null)
                    {
                        MeritDashBoardView ax = new MeritDashBoardView();
                        ax.memberId = deme.MemberId;
                        ax.point = deme.MemberDeMerits.Sum(x => x.Point);
                        deMListCond.Add(ax);
                    }
                    else
                    {
                        a.point += deme.MemberDeMerits.Sum(x => x.Point);
                    }
                }
                int maxvalc = deMListCond.Count() <= 0 ? 0 : deMListCond.Max(d => d.point);

                IEnumerable<DeMerit> deMeritDriv = _demerit.GetAll(z => z.Member.MemberType.Code == "Driver").ToList();
                deMeritDriv = deMeritDriv.Where(c => c.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && c.CreatedDate <= DateTime.Now.Date).OrderBy(s => s.MemberDeMerits.Sum(a => a.Point)).ToList();

                List<MeritDashBoardView> deMList = new List<MeritDashBoardView>();

                foreach (DeMerit deme in deMeritDriv)
                {
                    var a = deMListDri.Find(x => x.memberId == deme.MemberId);
                    if (a == null)
                    {
                        MeritDashBoardView ax = new MeritDashBoardView();
                        ax.memberId = deme.MemberId;
                        ax.point = deme.MemberDeMerits.Sum(x => x.Point);
                        deMListDri.Add(ax);
                    }
                    else
                    {
                        a.point += deme.MemberDeMerits.Sum(x => x.Point);
                    }
                }
                int maxvald = deMListDri.Count() <= 0 ? 0 : deMListDri.Max(d => d.point);

                HighestConductorPoints = deMListCond.Where(x => x.point >= maxvalc - 2 && x.point <= maxvalc + 2).Count();
                HighestDriverPoints = deMListDri.Where(x => x.point >= maxvald - 2 && x.point <= maxvald + 2).Count();

                if (dashboard != null)
                {

                    dashboardView.highestConductorComplain = highestConductorComplain;
                    dashboardView.highestDriverComplain = highestDriverComplain;
                    dashboardView.highestConductorPoints = HighestConductorPoints;
                    dashboardView.highestDriverPoints = HighestDriverPoints;
                    dashboardView.redNoticeConductors = rednoticeCon;// dashboard.RedNoticeConductors;
                    dashboardView.redNoticeDrivers = rednoticeDriver;// dashboard.RedNoticeDrivers;

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
