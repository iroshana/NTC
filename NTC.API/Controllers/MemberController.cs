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
    public class MemberController : ApiController
    {
        private readonly IMemberService _member;
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IDeMeritService _demerit;
        private readonly IComplainService _complain;

        public MemberController(IMemberService member, IEventLogService eventLog, ICommonDataService common, IDeMeritService demerit, IComplainService complain)
        {
            _member = member;
            _eventLog = eventLog;
            _common = common;
            _demerit = demerit;
            _complain = complain;
        }
        #region GetMember
        [HttpGet]
        public IHttpActionResult GetMember(int Id)
        {
            try
            {
                int redNoticePoint = 0;
                int bestMemberPoint = 0;
                bool rednotice = false;
                bool bestmember = true;
                MemberViewModel memberView = new MemberViewModel();
                Member member = new Member();
                member = _member.GetMember(Id);
                IEnumerable<DeMerit> demerit = new List<DeMerit>();
                List<MeritDashBoardView> meritIdlistDriver = new List<MeritDashBoardView>();

                demerit = _demerit.GetAll(x => x.MemberId == Id).ToList();
                if(demerit != null){
                    demerit = demerit.Where(y => y.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && y.CreatedDate <= DateTime.Now.Date).ToList();

                    foreach (DeMerit de in demerit)
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

                    rednotice = meritIdlistDriver.Where(x => x.point >= 2).Count() > 0 ? true : false;

                    
                    if (_complain.GetAll(x => x.ConductorId == Id || x.DriverId == Id).Count() > 2)
                    {
                        bestmember = false;
                    }
                }

                IEnumerable<Complain> complains = new List<Complain>();
                complains = _complain.GetAll(x=> x.ComplainStatus == "Unresolved" && (x.DriverId == Id || x.ConductorId == Id)).ToList();
                
                if (member != null)
                {
                    memberView.id = member.ID;
                    memberView.userID = member.UserID == null ? 0:member.UserID.Value;
                    memberView.fullName = String.IsNullOrEmpty(member.FullName)? String.Empty: member.FullName;
                    memberView.nameWithInitial = String.IsNullOrEmpty(member.ShortName) ? String.Empty : member.ShortName;
                    memberView.currentAddress = String.IsNullOrEmpty(member.CurrentAddress)? String.Empty : member.CurrentAddress;
                    memberView.permanetAddress = String.IsNullOrEmpty(member.PermanetAddress)? String.Empty : member.PermanetAddress;
                    memberView.nic = String.IsNullOrEmpty(member.NIC) ? String.Empty : member.NIC;
                    memberView.dob = member.DOB.ToString("yyyy-MM-dd");
                    memberView.cetificateNo = String.IsNullOrEmpty(member.TrainingCertificateNo) ? String.Empty : member.TrainingCertificateNo;
                    memberView.trainingCenter = String.IsNullOrEmpty(member.TrainingCenter) ? String.Empty : member.TrainingCenter;
                    memberView.licenceNo = String.IsNullOrEmpty(member.LicenceNo) ? String.Empty : member.LicenceNo;
                    memberView.dateIssued = member.IssuedDate.ToString("yyyy-MM-dd");
                    memberView.dateValidity = member.ExpireDate.ToString("yyyy-MM-dd");
                    memberView.dateJoin = member.JoinDate.ToString("yyyy-MM-dd");
                    memberView.educationQuali = String.IsNullOrEmpty(member.HighestEducation) ? String.Empty : member.HighestEducation;
                    memberView.type = member.MemberType.Code;
                    memberView.imagePath = member.ImagePath;
                    memberView.isNotification1 = rednotice;
                    memberView.notification1 = rednotice ? "Red Noticed " + member.MemberType.Code : String.Empty;
                    memberView.isNotification2 = bestmember && memberView.isNotification1  == false ? true : false;
                    memberView.notification2 = bestmember && memberView.isNotification1 == false ? "Best " + member.MemberType.Code : String.Empty;
                    memberView.isNotification3 = complains.Count() > 0 && complains != null? true : false;
                    memberView.notification3 = complains.Count() > 0 && complains != null ? complains.Count() + " Unresolved Complains" : String.Empty;
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { member = memberView, messageCode = messageData };
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

        #region GetMember
        [HttpGet]
        public IHttpActionResult GetAllMembers(int colorCode,DateTime? fromdate, DateTime? todate, int type,int point)
        {
            try
            {
                List<MemberEntityViewModel> memberListDriver = new List<MemberEntityViewModel>();
                List<MemberEntityViewModel> memberListConductor = new List<MemberEntityViewModel>();
                IEnumerable<MemberEntityModel> members = new List<MemberEntityModel>();
                members = _member.GetAllMembersSP(colorCode, fromdate, todate, type);
                if (members != null)
                {
                    foreach (MemberEntityModel member in members)
                    {
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = member.ID;
                        memberView.fullName = String.IsNullOrEmpty(member.FullName) ? String.Empty : member.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(member.TrainingCertificateNo) ? String.Empty : member.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(member.TrainingCenter) ? String.Empty : member.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(member.NIC) ? String.Empty : member.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(member.NTCNo) ? String.Empty : member.NTCNo;
                        memberView.typeCode = member.Code;
                        memberView.total = member.Total == null ? 0 : member.Total.Value;
                        if (point != 0 && memberView.total <= point)
                        {
                            if (member.Code == "Driver")
                            {
                                memberListDriver.Add(memberView);
                            }
                            else
                            {
                                memberListConductor.Add(memberView);
                            }
                        }
                        else if (point == 0)
                        {
                            if (member.Code == "Driver")
                            {
                                memberListDriver.Add(memberView);
                            }
                            else
                            {
                                memberListConductor.Add(memberView);
                            }
                        }

                    }
                }
                
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { memberListDriver = memberListDriver, memberListConductor = memberListConductor, messageCode = messageData };
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

        #region AddEmployee
        [HttpPost]
        public IHttpActionResult AddMember(MemberDataViewModel memberView)
        {
            try
            {
                string errorMessage = String.Empty;
                Member member = new Member();
                if (memberView != null)
                {
                    member.NIC = String.IsNullOrEmpty(memberView.nic) ? String.Empty: memberView.nic;
                    member.DOB = memberView.dob;
                    member.FullName = memberView.fullName;
                    member.ShortName = String.IsNullOrEmpty(memberView.nameWithInitial)? String.Empty : memberView.nameWithInitial;
                    member.PermanetAddress = String.IsNullOrEmpty(memberView.permanetAddress) ? String.Empty : memberView.permanetAddress;
                    member.CurrentAddress = String.IsNullOrEmpty(memberView.currentAddress) ? String.Empty : memberView.currentAddress;
                    member.TrainingCertificateNo = String.IsNullOrEmpty(memberView.cetificateNo) ? String.Empty : memberView.cetificateNo;
                    member.TrainingCenter = String.IsNullOrEmpty(memberView.trainingCenter) ? String.Empty : memberView.trainingCenter;
                    member.LicenceNo = String.IsNullOrEmpty(memberView.licenceNo)? String.Empty : memberView.licenceNo;
                    member.IssuedDate = memberView.dateIssued;
                    member.ExpireDate = memberView.dateValidity;
                    member.JoinDate = memberView.dateJoin;
                    member.HighestEducation = String.IsNullOrEmpty(memberView.educationQuali)? String.Empty : memberView.educationQuali;
                    member.TypeId = memberView.typeId;
                    member.NTCNo = _common.GetLastNTCNO(memberView.typeId);
                    member.ImagePath = memberView.imagePath;
                    _member.Add(member, out errorMessage);

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
                var returnObject = new {ntcNo = member.NTCNo, messageCode = messageData };
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

        #region GetAllMemberTypes
        [HttpGet]
        public IHttpActionResult GetAllMemberTypes()
        {
            try
            {
                List<MemberTypeViewModel> memberType = new List<MemberTypeViewModel>();
                IEnumerable<MemberType> types = new List<MemberType>();
                types = _common.GetAllMemberTypes();
                if (types.Count() > 0)
                {
                    foreach (MemberType type in types)
                    {
                        MemberTypeViewModel typeView = new MemberTypeViewModel();
                        typeView.id = type.ID;
                        typeView.code = type.Code;

                        memberType.Add(typeView);
                    }

                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { types = memberType, messageCode = messageData };
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

        #region GetLastNo
        [HttpGet]
        public IHttpActionResult GetLastNo()
        {
            try
            {
                string a = _common.GetLastNTCNO(2);

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { types = a, messageCode = messageData };
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

        #region GetAllRetnoticeMembers
        [HttpGet]
        public IHttpActionResult GetAllRetnoticeMembers(int colorCode, DateTime? fromdate, DateTime? todate, int type, int point)
        {
            try
            {
                List<MemberEntityViewModel> memberListDriver = new List<MemberEntityViewModel>();
                List<MemberEntityViewModel> memberListConductor = new List<MemberEntityViewModel>();
               
                //members = _member.GetAllMembersSP(colorCode, fromdate, todate, type);

                //if (members != null)
                //{
                //    foreach (MemberEntityModel member in members)
                //    {
                //        MemberEntityViewModel memberView = new MemberEntityViewModel();

                //        memberView.id = member.ID;
                //        memberView.fullName = String.IsNullOrEmpty(member.FullName) ? String.Empty : member.FullName;
                //        memberView.trainingCertificateNo = String.IsNullOrEmpty(member.TrainingCertificateNo) ? String.Empty : member.TrainingCertificateNo;
                //        memberView.trainingCenter = String.IsNullOrEmpty(member.TrainingCenter) ? String.Empty : member.TrainingCenter;
                //        memberView.nic = String.IsNullOrEmpty(member.NIC) ? String.Empty : member.NIC;
                //        memberView.ntcNo = String.IsNullOrEmpty(member.NTCNo) ? String.Empty : member.NTCNo;
                //        memberView.typeCode = member.Code;
                //        memberView.total = member.Total == null ? 0 : member.Total.Value;
                //        if (point != 0 && memberView.total >= point)
                //        {
                //            if (member.Code == "Driver")
                //            {
                //                memberListDriver.Add(memberView);
                //            }
                //            else
                //            {
                //                memberListConductor.Add(memberView);
                //            }
                //        }
                //        else if (point == 0)
                //        {
                //            if (member.Code == "Driver")
                //            {
                //                memberListDriver.Add(memberView);
                //            }
                //            else
                //            {
                //                memberListConductor.Add(memberView);
                //            }
                //        }

                //    }
                //}


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
                            var a = meritIdlistDriver.Find(x => x.memberId == de.MemberId && x.id == memdem.MeritId);
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
                            var a = meritIdlistCond.Find(x => x.memberId == de.MemberId && x.id == memdem.MeritId);
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
                List<int> memb = new List<int>();
                
                if (type == 1)
                {
                    memb = meritIdlistDriver.Where(r => r.point > 2).Select(z => z.memberId).Distinct().ToList();
                    foreach (int s in memb)
                    {
                        Member mem = _member.GetAll(x => x.ID == s).FirstOrDefault();
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = mem.ID;
                        memberView.fullName = String.IsNullOrEmpty(mem.FullName) ? String.Empty : mem.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(mem.TrainingCertificateNo) ? String.Empty : mem.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(mem.TrainingCenter) ? String.Empty : mem.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(mem.NIC) ? String.Empty : mem.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(mem.NTCNo) ? String.Empty : mem.NTCNo;
                        memberView.typeCode = mem.MemberType.Code;

                        memberListDriver.Add(memberView);
                    }
                }
                else
                {
                    memb = meritIdlistCond.Where(r => r.point > 2).Select(z => z.memberId).Distinct().ToList();
                    foreach (int s in memb)
                    {
                        Member mem = _member.GetAll(x => x.ID == s).FirstOrDefault();
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = mem.ID;
                        memberView.fullName = String.IsNullOrEmpty(mem.FullName) ? String.Empty : mem.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(mem.TrainingCertificateNo) ? String.Empty : mem.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(mem.TrainingCenter) ? String.Empty : mem.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(mem.NIC) ? String.Empty : mem.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(mem.NTCNo) ? String.Empty : mem.NTCNo;
                        memberView.typeCode = mem.MemberType.Code;

                        memberListConductor.Add(memberView);
                    }
                }
                
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { memberListDriver = memberListDriver, memberListConductor = memberListConductor, messageCode = messageData };
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

        #region GetAllRetnoticeMembers
        [HttpGet]
        public IHttpActionResult GetAllBestMembers(DateTime date, int type, bool isMonth)
        {
            try
            {
                List<MemberEntityViewModel> memberListDriver = new List<MemberEntityViewModel>();
                List<MemberEntityViewModel> memberListConductor = new List<MemberEntityViewModel>();
                IEnumerable<MemberEntityModel> members = new List<MemberEntityModel>();

                //members = _member.GetAllMembersSP(0, (DateTime?)null, (DateTime?)null, type);
                List<Member> membesDateList = new List<Member>();
                IEnumerable<Member> memberList = new List<Member>();
                memberList = _member.GetAll(z=>z.TypeId == type).ToList();
                foreach (Member mem in memberList)
                {
                    int point = 0;
                    IEnumerable<DeMerit> memberDe = _demerit.GetAll(x => x.MemberId == mem.ID).ToList();
                    if (isMonth)
                    {
                        memberDe = memberDe.Where(x => x.CreatedDate.Date >= date.Date.AddMonths(-1) && x.CreatedDate.Date <= date.Date).ToList();
                        if (memberDe.Count() < 2)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID || x.ConductorId == mem.ID).Count() < 2)
                            {
                                membesDateList.Add(mem);
                            }
                        }
                        else if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID || x.ConductorId == mem.ID).Count() < 2)
                            {
                                membesDateList.Add(mem);
                            }
                        }
                    }
                    else
                    {
                        memberDe = memberDe.Where(x => x.CreatedDate.Date >= date.Date.AddYears(-1) && x.CreatedDate.Date <= date.Date).ToList();
                        if (memberDe.Count() < 2)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID || x.ConductorId == mem.ID).Count() < 2)
                            {
                                membesDateList.Add(mem);
                            }
                        }
                        else if (memberDe.Count() == 0)
                        {
                            if (_complain.GetAll(x => x.DriverId == mem.ID || x.ConductorId == mem.ID).Count() < 2)
                            {
                                membesDateList.Add(mem);
                            }
                        }
                    }
                   
                }
                if (members != null)
                {
                    foreach (Member member in membesDateList)
                    {
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = member.ID;
                        memberView.fullName = String.IsNullOrEmpty(member.FullName) ? String.Empty : member.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(member.TrainingCertificateNo) ? String.Empty : member.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(member.TrainingCenter) ? String.Empty : member.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(member.NIC) ? String.Empty : member.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(member.NTCNo) ? String.Empty : member.NTCNo;
                        memberView.typeCode = member.MemberType.Code;
                        //memberView.total = member.Total == null ? 0 : member.Total.Value;
                        if (member.MemberType.Code == "Driver")
                        {
                            memberListDriver.Add(memberView);
                        }
                        else
                        {
                            memberListConductor.Add(memberView);
                        }

                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { memberListDriver = memberListDriver, memberListConductor = memberListConductor, messageCode = messageData };
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

        #region GetAllHightsNoOfComplainMembers
        [HttpGet]
        public IHttpActionResult GetAllHightsNoOfComplainMembers(int typeId)
        {
            try
            {
                List<MemberEntityViewModel> memberListDriver = new List<MemberEntityViewModel>();
                List<MemberEntityViewModel> memberListConductor = new List<MemberEntityViewModel>();
                IEnumerable<MemberEntityModel> members = new List<MemberEntityModel>();
                List<MeritDashBoardView> highDriver = new List<MeritDashBoardView>();
                List<MeritDashBoardView> highCond = new List<MeritDashBoardView>();

                IEnumerable<Complain> driverComplain = _complain.GetAll(x => x.Member.MemberType.Code == "Driver" && x.ComplainStatus != "Resolve");
                IEnumerable<Complain> conductComplain = _complain.GetAll(x => x.Member1.MemberType.Code == "Conductor" && x.ComplainStatus != "Resolve");

                foreach (Complain dcomp in driverComplain)
                {
                    var a = highDriver.Find(x => x.memberId == dcomp.DriverId);
                    if (a == null && dcomp.DriverId != null)
                    {
                        MeritDashBoardView md = new MeritDashBoardView();
                        md.point = 1;
                        md.memberId = dcomp.DriverId.Value;
                        highDriver.Add(md);

                    }else
                    {
                        a.point++;
                    }
                }

                foreach (Complain dcomp in conductComplain)
                {
                    var a = highCond.Find(x => x.memberId == dcomp.DriverId);
                    if (a == null && dcomp.DriverId != null)
                    {
                        MeritDashBoardView md = new MeritDashBoardView();
                        md.point = 1;
                        md.memberId = dcomp.DriverId.Value;
                        highCond.Add(md);
                    }
                    else
                    {
                        a.point++;
                    }
                }

                highDriver = highDriver.Where(w => w.point > 10).ToList();
                highCond = highCond.Where(w => w.point > 10).ToList();

                if (typeId == 1)
                {
                    foreach (MeritDashBoardView mDriver in highDriver)
                    {
                        Member mem = _member.GetAll(x => x.ID == mDriver.memberId).FirstOrDefault();
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = mem.ID;
                        memberView.fullName = String.IsNullOrEmpty(mem.FullName) ? String.Empty : mem.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(mem.TrainingCertificateNo) ? String.Empty : mem.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(mem.TrainingCenter) ? String.Empty : mem.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(mem.NIC) ? String.Empty : mem.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(mem.NTCNo) ? String.Empty : mem.NTCNo;
                        memberView.typeCode = mem.MemberType.Code;

                        memberListDriver.Add(memberView);
                    }
                }
                else
                {
                    foreach (MeritDashBoardView mConduct in highCond)
                    {
                        Member mem = _member.GetAll(x => x.ID == mConduct.memberId).FirstOrDefault();
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = mem.ID;
                        memberView.fullName = String.IsNullOrEmpty(mem.FullName) ? String.Empty : mem.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(mem.TrainingCertificateNo) ? String.Empty : mem.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(mem.TrainingCenter) ? String.Empty : mem.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(mem.NIC) ? String.Empty : mem.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(mem.NTCNo) ? String.Empty : mem.NTCNo;
                        memberView.typeCode = mem.MemberType.Code;

                        memberListConductor.Add(memberView);
                    }
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { memberListDriver = memberListDriver, memberListConductor = memberListConductor, messageCode = messageData };
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
        #region GetAllHightsPointMembers
        [HttpGet]
        public IHttpActionResult GetAllHightsPointMembers(int typeId)
        {
            try
            {
                List<MemberEntityViewModel> memberListDriver = new List<MemberEntityViewModel>();
                List<MemberEntityViewModel> memberListConductor = new List<MemberEntityViewModel>();
                List<MeritDashBoardView> deMListCond = new List<MeritDashBoardView>();
                List<MeritDashBoardView> deMListDri = new List<MeritDashBoardView>();

                IEnumerable<DeMerit> deMeritCond = _demerit.GetAll(z => z.Member.MemberType.Code == "Conductor").ToList();
                deMeritCond = deMeritCond.Where(c => c.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && c.CreatedDate <= DateTime.Now.Date).ToList();

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

                deMListCond = deMListCond.Where(x => x.point >= maxvalc - 2 && x.point <= maxvalc + 2).ToList();
                deMListDri = deMListDri.Where(x => x.point >= maxvald - 2 && x.point <= maxvald + 2).ToList();

                if (typeId == 1)
                {
                    foreach (MeritDashBoardView mDriver in deMListDri)
                    {
                        Member mem = _member.GetAll(x => x.ID == mDriver.memberId).FirstOrDefault();
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = mem.ID;
                        memberView.fullName = String.IsNullOrEmpty(mem.FullName) ? String.Empty : mem.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(mem.TrainingCertificateNo) ? String.Empty : mem.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(mem.TrainingCenter) ? String.Empty : mem.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(mem.NIC) ? String.Empty : mem.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(mem.NTCNo) ? String.Empty : mem.NTCNo;
                        memberView.typeCode = mem.MemberType.Code;

                        memberListDriver.Add(memberView);
                    }
                }
                else
                {
                    foreach (MeritDashBoardView mConduct in deMListCond)
                    {
                        Member mem = _member.GetAll(x => x.ID == mConduct.memberId).FirstOrDefault();
                        MemberEntityViewModel memberView = new MemberEntityViewModel();

                        memberView.id = mem.ID;
                        memberView.fullName = String.IsNullOrEmpty(mem.FullName) ? String.Empty : mem.FullName;
                        memberView.trainingCertificateNo = String.IsNullOrEmpty(mem.TrainingCertificateNo) ? String.Empty : mem.TrainingCertificateNo;
                        memberView.trainingCenter = String.IsNullOrEmpty(mem.TrainingCenter) ? String.Empty : mem.TrainingCenter;
                        memberView.nic = String.IsNullOrEmpty(mem.NIC) ? String.Empty : mem.NIC;
                        memberView.ntcNo = String.IsNullOrEmpty(mem.NTCNo) ? String.Empty : mem.NTCNo;
                        memberView.typeCode = mem.MemberType.Code;

                        memberListConductor.Add(memberView);
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { memberListDriver = memberListDriver, memberListConductor = memberListConductor, messageCode = messageData };
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
