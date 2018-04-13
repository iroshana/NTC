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

        public MemberController(IMemberService member, IEventLogService eventLog, ICommonDataService common)
        {
            _member = member;
            _eventLog = eventLog;
            _common = common;
        }
        #region GetMember
        [HttpGet]
        public IHttpActionResult GetMember(int Id)
        {
            try
            {
                MemberViewModel memberView = new MemberViewModel();
                Member member = new Member();
                member = _member.GetMember(Id);
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
        public IHttpActionResult GetAllMembers(int colorCode,DateTime? fromdate, DateTime? todate, int type)
        {
            try
            {
                List<MemberEntityViewModel> memberList = new List<MemberEntityViewModel>();
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

                        memberList.Add(memberView);
                    }
                }
                
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { members = memberList, messageCode = messageData };
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
        public IHttpActionResult AddMember(MemberViewModel memberView)
        {
            try
            {
                string errorMessage = String.Empty;
                Member member = new Member();
                if (memberView != null)
                {
                    member.NIC = String.IsNullOrEmpty(memberView.nic) ? String.Empty: memberView.nic;
                    member.DOB = DateTime.Parse(memberView.dob);
                    member.FullName = memberView.fullName;
                    member.ShortName = String.IsNullOrEmpty(memberView.nameWithInitial)? String.Empty : memberView.nameWithInitial;
                    member.PermanetAddress = String.IsNullOrEmpty(memberView.permanetAddress) ? String.Empty : memberView.permanetAddress;
                    member.CurrentAddress = String.IsNullOrEmpty(memberView.currentAddress) ? String.Empty : memberView.currentAddress;
                    member.TrainingCertificateNo = String.IsNullOrEmpty(memberView.cetificateNo) ? String.Empty : memberView.cetificateNo;
                    member.TrainingCenter = String.IsNullOrEmpty(memberView.trainingCenter) ? String.Empty : memberView.trainingCenter;
                    member.LicenceNo = String.IsNullOrEmpty(memberView.licenceNo)? String.Empty : memberView.trainingCenter;
                    member.IssuedDate = DateTime.Parse(memberView.dateIssued);
                    member.ExpireDate = DateTime.Parse(memberView.dateValidity);
                    member.JoinDate = DateTime.Parse(memberView.dateJoin);
                    member.HighestEducation = String.IsNullOrEmpty(memberView.educationQuali)? String.Empty : memberView.educationQuali;
                    member.TypeId = memberView.typeId;
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
    }
}
