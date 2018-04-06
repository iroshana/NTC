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

                    deMeritView.MemberDeMerit = new List<MemberDeMeritViewModel>();
                    foreach (MemberDeMerit merti in deMerit.MemberDeMerits)
                    {
                        MemberDeMeritViewModel demerit = new MemberDeMeritViewModel();
                        demerit.id = merti.ID;
                        demerit.meritId = merti.MeritId;
                        demerit.code = merti.Merit.Code;
                        demerit.description = merti.Merit.Description;
                        demerit.point = merti.Point;
                        demerit.colorCode = merti.Merit.ColorCodeId;

                        deMeritView.MemberDeMerit.Add(demerit);
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
        public IHttpActionResult GetDemeritByMemberId(int Id)
        {
            try
            {
                List<DeMeritViewModel> deMeritList = new List<DeMeritViewModel>();
                IEnumerable<DeMerit> deMerits = new List<DeMerit>();
                deMerits = _deMerit.GetDeMeritByUser(Id);
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
    }
}
