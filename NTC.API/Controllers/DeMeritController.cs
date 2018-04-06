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
        public IHttpActionResult GetDemeritByNo(string deMeritNo)
        {
            try
            {
                DeMeritViewModel deMeritView = new DeMeritViewModel();
                DeMerit deMerit = new DeMerit();
                deMerit = _deMerit.GetDeMeritNo(deMeritNo);
                if (deMerit != null)
                {
                    deMeritView.id = deMerit.ID;
                    deMeritView.inqueryDate = deMerit.InqueryDate.ToString(@"yyyy-MM-dd");
                    deMeritView.member = new MemberViewModel();
                    deMeritView.member.id = deMerit.MemberId;
                    deMeritView.member.nameWithInitial = deMerit.Member.ShortName;


                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { member = deMeritView, messageCode = messageData };
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

        #region GetDemeritMemberId
        [HttpGet]
        public IHttpActionResult GetDemeritMemberId(int Id)
        {
            try
            {
                MemberViewModel memberView = new MemberViewModel();
                Member member = new Member();
                member = _member.GetMember(Id);
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
    }
}
