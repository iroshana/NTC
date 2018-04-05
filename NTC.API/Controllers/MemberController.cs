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
                member = _member.GetEmployee(Id);
                if (member != null)
                {
                    memberView.id = member.ID;
                    memberView.userID = member.UserID.Value;
                    memberView.fullName = member.FullName;
                    memberView.currentAddress = member.CurrentAddress;
                    memberView.permanetAddress = member.PermanetAddress;
                    memberView.nic = member.NIC;
                    memberView.dob = member.DOB.ToString("yyyy-MM-dd");
                    memberView.cetificateNo = member.TrainingCertificateNo;
                    memberView.trainingCenter = member.TrainingCenter;
                    memberView.licenceNo = member.LicenceNo;
                    memberView.dateIssued = member.IssuedDate.ToString("yyyy-MM-dd");
                    memberView.dateValidity = member.ExpireDate.ToString("yyyy-MM-dd");
                    memberView.dateJoin = member.JoinDate.ToString("yyyy-MM-dd");
                    memberView.educationQuali = member.HighestEducation;
                    memberView.type = member.MemberType.Code;
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { item = memberView, messageCode = messageData };
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
        public IHttpActionResult GetAllMember(int Id)
        {
            try
            {
                MemberViewModel memberView = new MemberViewModel();
                Member member = new Member();
                member = _member.GetEmployee(Id);
                if (member != null)
                {
                    memberView.id = member.ID;
                    memberView.userID = member.UserID.Value;
                    memberView.fullName = member.FullName;
                    memberView.currentAddress = member.CurrentAddress;
                    memberView.permanetAddress = member.PermanetAddress;
                    memberView.nic = member.NIC;
                    memberView.dob = member.DOB.ToString("yyyy-MM-dd");
                    memberView.cetificateNo = member.TrainingCertificateNo;
                    memberView.trainingCenter = member.TrainingCenter;
                    memberView.licenceNo = member.LicenceNo;
                    memberView.dateIssued = member.IssuedDate.ToString("yyyy-MM-dd");
                    memberView.dateValidity = member.ExpireDate.ToString("yyyy-MM-dd");
                    memberView.dateJoin = member.JoinDate.ToString("yyyy-MM-dd");
                    memberView.educationQuali = member.HighestEducation;

                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { item = memberView, messageCode = messageData };
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
        [HttpGet]
        public IHttpActionResult AddEmployee(MemberViewModel memberView)
        {
            try
            {
                string errorMessage = String.Empty;
                Member member = new Member();
                if (memberView != null)
                {
                    member.NIC = memberView.nic;
                    member.DOB = DateTime.Parse(memberView.dob);
                    member.FullName = memberView.fullName;
                    member.ShortName = memberView.nameWithInitial;
                    member.PermanetAddress = memberView.permanetAddress;
                    member.CurrentAddress = memberView.currentAddress;
                    member.TrainingCertificateNo = memberView.cetificateNo;
                    member.TrainingCenter = memberView.trainingCenter;
                    member.LicenceNo = memberView.licenceNo;
                    member.IssuedDate = DateTime.Parse(memberView.dateIssued);
                    member.ExpireDate = DateTime.Parse(memberView.dateValidity);
                    member.JoinDate = DateTime.Parse(memberView.dateJoin);
                    member.HighestEducation = memberView.educationQuali;

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
