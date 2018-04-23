﻿using NTC.BusinessEntities;
using NTC.InterfaceServices;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace NTC.API.Controllers
{
    public class DeMeritController : ApiController
    {
        private readonly IMemberService _member;
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IDeMeritService _deMerit;

        public DeMeritController(IMemberService member, IEventLogService eventLog, ICommonDataService common, IDeMeritService deMerit)
        {
            _member = member;
            _eventLog = eventLog;
            _common = common;
            _deMerit = deMerit;
        }
        #region GetDemeritByNo
        [HttpGet]
        public IHttpActionResult GetDemeritByNo(int deMeritNo)
        {
            try
            {
                DeMeritViewModel deMeritView = new DeMeritViewModel();
                DeMerit deMerit = new DeMerit();
                deMerit = _deMerit.GetDeMeritByNo(deMeritNo);
                if (deMerit != null)
                {
                    deMeritView.id = deMerit.ID;
                    deMeritView.deMeritNo = deMerit.DeMeritNo;
                    deMeritView.inqueryDate = deMerit.InqueryDate.ToString(@"yyyy-MM-dd");
                    deMeritView.member = new MemberViewModel();
                    deMeritView.member.id = deMerit.MemberId;
                    deMeritView.member.nameWithInitial = deMerit.Member.ShortName;
                    deMeritView.inqueryDate = deMerit.InqueryDate.ToString(@"yyyy-MM-dd");
                    deMeritView.officer = new OfficerViewModel();
                    deMeritView.officer.id = deMerit.Officer.ID;
                    deMeritView.officer.name = deMerit.Officer.Name;
                    deMeritView.bus = new BusViewModel();
                    deMeritView.bus.id = deMerit.Bus.ID;
                    deMeritView.bus.busNo = deMerit.Bus.LicenceNo;

                    deMeritView.memberDeMerit = new List<MemberDeMeritViewModel>();
                    foreach (MemberDeMerit merti in deMerit.MemberDeMerits)
                    {
                        MemberDeMeritViewModel demerit = new MemberDeMeritViewModel();
                        demerit.id = merti.ID;
                        demerit.meritId = merti.MeritId;
                        demerit.code = merti.Merit.Code;
                        demerit.description = merti.Merit.Description;
                        demerit.point = merti.Point;
                        demerit.colorCode = merti.Merit.ColorCodeId;

                        deMeritView.memberDeMerit.Add(demerit);
                    }
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { demerit = deMeritView, messageCode = messageData };
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

        #region GetDemeritByMemberId
        [HttpGet]
        public IHttpActionResult GetDemeritByMemberId(int memberId)
        {
            try
            {
                List<DeMeritViewModel> deMeritList = new List<DeMeritViewModel>();
                IEnumerable<DeMerit> deMerits = new List<DeMerit>();
                deMerits = _deMerit.GetDeMeritByUser(memberId);
                if (deMerits != null)
                {
                    foreach (DeMerit deMerit in deMerits)
                    {
                        DeMeritViewModel deMeritView = new DeMeritViewModel();
                        deMeritView.id = deMerit.ID;
                        deMeritView.deMeritNo = deMerit.DeMeritNo;

                        deMeritList.Add(deMeritView);
                    }
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { demerit = deMeritList, messageCode = messageData };
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

        #region AddDeMerit
        [HttpPost]
        public IHttpActionResult AddDeMerit(DeMeritViewModel deMeritView)
        {
            try
            {
                string errorMessage = String.Empty;
                DeMerit deMerit = new DeMerit();
                if (deMeritView != null)
                {
                    deMerit.DeMeritNo = deMeritView.deMeritNo;
                    deMerit.InqueryDate = DateTime.Parse(deMeritView.inqueryDate);
                    deMerit.MemberId = deMeritView.member.id;
                    deMerit.BusId = deMeritView.bus.id;
                    deMerit.RouteId = deMeritView.route.id;
                    deMerit.OfficeriId = deMeritView.officer.id;
                    deMerit.CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                    deMerit.MemberDeMerits = new List<MemberDeMerit>();
                    foreach (MemberDeMeritViewModel memberDemerit in deMeritView.memberDeMerit)
                    {
                        MemberDeMerit demerit = new MemberDeMerit();
                        demerit.DeMeritId = deMerit.ID;
                        demerit.MeritId = memberDemerit.meritId;
                        if (memberDemerit.isSelected)
                        {
                            demerit.Point = 1;
                        }
                        else
                        {
                            demerit.Point = 0;
                        }
                        

                        deMerit.MemberDeMerits.Add(demerit);
                    }

                    _deMerit.Add(deMerit,out errorMessage);

                }
                else
                {
                    errorMessage = Constant.MessageGeneralError;
                }

                var messageData = new
                {
                    code = String.IsNullOrEmpty(errorMessage) ? Constant.SuccessMessageCode : Constant.ErrorMessageCode
                   ,
                    message = String.IsNullOrEmpty(errorMessage) ? Constant.MessageSuccess : errorMessage
                };
                var returnObject = new { messageCode = messageData };
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

        #region GetLastDemeritNo
        [HttpGet]
        public IHttpActionResult GetLastDemeritNo()
        {
            try
            {
                DeMerit deMerit = new DeMerit();
                deMerit = _deMerit.GetLastDeMeritNo();
                DateTime date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));


                string today = date.ToShortDateString().Replace("/", String.Empty);
                string nextdeMeritNo = String.Empty;

                if (deMerit != null)
                {
                    string deMeritNo = deMerit.DeMeritNo;

                    string deMeritdate = deMeritNo.Split('-')[0];
                    int no = int.Parse(deMeritNo.Split('-')[1]);

                    if (deMeritdate == today)
                    {
                        nextdeMeritNo = deMeritdate + "-" + (no + 1);
                    }
                    else
                    {
                        nextdeMeritNo = today + "-" + "1";
                    }
                }
                else
                {
                    nextdeMeritNo = today + "-" + "1";
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainNo = nextdeMeritNo, messageCode = messageData };
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

        #region GetAllMerits
        [HttpGet]
        public IHttpActionResult GetAllMerits()
        {
            try
            {
                List<MemberDeMeritViewModel> meritList = new List<MemberDeMeritViewModel>();
                IEnumerable<Merit> merits = new List<Merit>();
                merits = _common.GetAllMerits();

                foreach (Merit merit in merits)
                {
                    MemberDeMeritViewModel meritView = new MemberDeMeritViewModel();
                    meritView.meritId = merit.ID;
                    meritView.code = merit.Code;
                    meritView.description = merit.Description;
                    meritView.colorCode = merit.ColorCodeId;
                    meritView.point = 0;

                    meritList.Add(meritView);
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { merits = meritList, messageCode = messageData };
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

        #region GetPanelties
        [HttpGet]
        public IHttpActionResult GetPanelties()
        {
            try
            {
                IEnumerable<DeMerit> deMerits = new List<DeMerit>();
                deMerits = _deMerit.GetAllDeMerits();
                DeMeritMemberTypeViewModel deMeritMemType = new DeMeritMemberTypeViewModel();
                deMeritMemType.driver = new DeMeritTypeSetViewModel();
                deMeritMemType.driver.adPannel = new List<DeMeritTypeViewModel>();
                deMeritMemType.driver.finePay = new List<DeMeritTypeViewModel>();
                deMeritMemType.driver.punish = new List<DeMeritTypeViewModel>();
                deMeritMemType.driver.cancel = new List<DeMeritTypeViewModel>();


                deMeritMemType.conductor = new DeMeritTypeSetViewModel();
                deMeritMemType.conductor.adPannel = new List<DeMeritTypeViewModel>();
                deMeritMemType.conductor.finePay = new List<DeMeritTypeViewModel>();
                deMeritMemType.conductor.punish = new List<DeMeritTypeViewModel>();
                deMeritMemType.conductor.cancel = new List<DeMeritTypeViewModel>();


                foreach (DeMerit demerit in deMerits)
                {
                    foreach (MemberDeMerit mem in demerit.MemberDeMerits)
                    {
                        switch (demerit.Member.MemberType.Code)
                        {
                            case "Driver":
                                switch (mem.Merit.ColorCodeId)
                                {
                                    case 1:
                                        var a = deMeritMemType.driver.cancel.Find(x => x.id == demerit.Member.ID);
                                        if (a == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.driver.cancel.Add(ad);
                                        }
                                        break;
                                    case 2:
                                        var b = deMeritMemType.driver.adPannel.Find(x => x.id == demerit.Member.ID);
                                        if (b == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.driver.adPannel.Add(ad);
                                        }
                                        break;
                                    case 3:
                                        var c = deMeritMemType.driver.punish.Find(x => x.id == demerit.Member.ID);
                                        if (c == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.driver.punish.Add(ad);
                                        }
                                        break;
                                    case 4:
                                        var d = deMeritMemType.driver.finePay.Find(x => x.id == demerit.Member.ID);
                                        if (d == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.driver.finePay.Add(ad);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case "Conductor":
                                switch (mem.Merit.ColorCodeId)
                                {
                                    case 1:
                                        var a = deMeritMemType.conductor.cancel.Find(x => x.id == demerit.Member.ID);
                                        if (a == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.conductor.cancel.Add(ad);
                                        }
                                        break;
                                    case 2:
                                        var b = deMeritMemType.conductor.adPannel.Find(x => x.id == demerit.Member.ID);
                                        if (b == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.conductor.adPannel.Add(ad);
                                        }
                                        break;
                                    case 3:
                                        var c = deMeritMemType.conductor.punish.Find(x => x.id == demerit.Member.ID);
                                        if (c == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.conductor.punish.Add(ad);
                                        }
                                        break;
                                    case 4:
                                        var d = deMeritMemType.conductor.finePay.Find(x => x.id == demerit.Member.ID);
                                        if (d == null && mem.Point > 2)
                                        {
                                            DeMeritTypeViewModel ad = new DeMeritTypeViewModel();
                                            ad.id = demerit.Member.ID;
                                            ad.name = demerit.Member.FullName;
                                            ad.ntcNo = demerit.Member.NTCNo;
                                            deMeritMemType.conductor.finePay.Add(ad);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            default : break;
                        }
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { merits = deMeritMemType, messageCode = messageData };
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

        #region Get Officer
        public IHttpActionResult GetOfficer(string name)
        {
            try
            {
                OfficerViewModel officerVM = new OfficerViewModel();
                Officer officer = _common.GetOfficer(name);

                if (officer != null)
                {
                    officerVM.id = officer.ID;
                    officerVM.name = officer.Name;
                    officerVM.nic = officer.NIC;
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { officer = officerVM, messageCode = messageData };
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

        #region GetBestMember
        [HttpGet]
        public IHttpActionResult GetBestMember()
        {
            try
            {
                DeMerit deMerit = new DeMerit();
                deMerit = _deMerit.GetLastDeMeritNo();
                DateTime date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));


                string today = date.ToShortDateString().Replace("/", String.Empty);
                string nextdeMeritNo = String.Empty;

                if (deMerit != null)
                {
                    string deMeritNo = deMerit.DeMeritNo;

                    string deMeritdate = deMeritNo.Split('-')[0];
                    int no = int.Parse(deMeritNo.Split('-')[1]);

                    if (deMeritdate == today)
                    {
                        nextdeMeritNo = deMeritdate + "-" + (no + 1);
                    }
                    else
                    {
                        nextdeMeritNo = today + "-" + "1";
                    }
                }
                else
                {
                    nextdeMeritNo = today + "-" + "1";
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainNo = nextdeMeritNo, messageCode = messageData };
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

        #region GetDemeritSummery
        [HttpGet]
        public IHttpActionResult GetDemeritSummery(int memberId)
        {
            try
            {
                IList<MemberDeMeritViewModel> memberDemeritView = new List<MemberDeMeritViewModel>();
                memberDemeritView = _deMerit.GetDeMeritSummery(memberId);


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { merits = memberDemeritView, messageCode = messageData };
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

        #region Chart
        [HttpGet]
        public IHttpActionResult ChartData()
        {
            try
            {
                ChartDeMeritCountViewModel chart = new ChartDeMeritCountViewModel();
                //chart.adPannel = new ChartDeMeritViewModel();
                //chart.cancel = new ChartDeMeritViewModel();
                //chart.punish = new ChartDeMeritViewModel();
                //chart.finePay = new ChartDeMeritViewModel();
                chart.adPannel = new List<int>();
                chart.cancel = new List<int>();
                chart.punish = new List<int>();
                chart.finePay = new List<int>();

                IEnumerable<MemberDeMerit> deMerits = new List<MemberDeMerit>();
                deMerits = _deMerit.GetDeMerits();
                //deMerits = deMerits.Where(x => x.DeMerit.CreatedDate.Year == DateTime.Now.Year && x.DeMerit.CreatedDate.Month == 1).Select(x => x.DeMerit.MemberId).Count();//.ToList();

                if (deMerits != null)
                {
                    for (int i = 1;i<=12; i++)
                    {
                        chart.adPannel.Add(deMerits.Where(x => x.DeMerit.CreatedDate.Year == DateTime.Now.Year && x.DeMerit.CreatedDate.Month == i && x.Merit.ColorCodeId == 2).Select(x => x.DeMerit.MemberId).Distinct().Count());

                        chart.finePay.Add(deMerits.Where(x => x.DeMerit.CreatedDate.Year == DateTime.Now.Year && x.DeMerit.CreatedDate.Month == i && x.Merit.ColorCodeId == 4).Select(x => x.DeMerit.MemberId).Distinct().Count());

                        chart.punish.Add(deMerits.Where(x => x.DeMerit.CreatedDate.Year == DateTime.Now.Year && x.DeMerit.CreatedDate.Month == i && x.Merit.ColorCodeId == 3).Select(x => x.DeMerit.MemberId).Distinct().Count());

                        chart.cancel.Add(deMerits.Where(x => x.DeMerit.CreatedDate.Year == DateTime.Now.Year && x.DeMerit.CreatedDate.Month == i && x.Merit.ColorCodeId == 1).Select(x => x.DeMerit.MemberId).Distinct().Count());
                    }

                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { chart = chart, messageCode = messageData };
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
