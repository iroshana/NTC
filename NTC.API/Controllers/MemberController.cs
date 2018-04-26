﻿using NTC.BusinessEntities;
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
                MemberViewModel memberView = new MemberViewModel();
                Member member = new Member();
                member = _member.GetMember(Id);
                IEnumerable<DeMerit> demerit = new List<DeMerit>();
                demerit = _demerit.GetAll(x => x.MemberId == Id).ToList();
                if(demerit != null){
                    demerit = demerit.Where(y => y.CreatedDate.Date >= DateTime.Now.Date.AddMonths(-1) && y.CreatedDate <= DateTime.Now.Date).ToList();

                    foreach (DeMerit de in demerit)
                    {
                        foreach (MemberDeMerit mem in de.MemberDeMerits)
                        {
                            if (mem.Merit.ColorCodeId == 1)
                            {
                                redNoticePoint += mem.Point;
                            }
                        }
                    }
                    foreach (DeMerit de in demerit)
                    {
                        foreach (MemberDeMerit mem in de.MemberDeMerits)
                        {
                            bestMemberPoint += mem.Point;
                        }
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
                    memberView.isNotification1 = redNoticePoint > 2 ? true : false;
                    memberView.notification1 = redNoticePoint > 2 ? "Red Noticed " + member.MemberType.Code : String.Empty;
                    memberView.isNotification2 = bestMemberPoint < 2 && memberView.isNotification1  == false ? true : false;
                    memberView.notification2 = bestMemberPoint < 2 && memberView.isNotification1 == false ? "Best " + member.MemberType.Code : String.Empty;
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
                        if (point != 0 && memberView.total >= point)
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
                if (isMonth)
                {
                    members = _member.GetBestMembersSP(date.AddMonths(-1), date, type);
                }
                else
                {
                    members = _member.GetBestMembersSP(date.AddYears(-1), date, type);
                }
                
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
