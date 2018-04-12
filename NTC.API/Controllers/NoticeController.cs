using NTC.BusinessEntities;
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
    public class NoticeController : ApiController
    {
        private readonly INoticeService _notice;
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;

        public NoticeController(INoticeService notice, IEventLogService eventLog, ICommonDataService common)
        {
            _notice = notice;
            _eventLog = eventLog;
            _common = common;
        }

        #region GetAllNotices
        [HttpGet]
        public IHttpActionResult GetAllNotices()
        {
            try
            {
                List<NoticeViewModel> noticeListVM = new List<NoticeViewModel>();
                IEnumerable<Notice> notice = new List<Notice>();
                notice = _notice.GetAllNotices();
                if (notice != null)
                {
                    foreach (Notice Note in notice)
                    {
                        NoticeViewModel noticeVM = new NoticeViewModel();
                        noticeVM.Content = Note.Content;
                        noticeVM.NoticeCode = Note.NoticeCode;
                        noticeVM.ID = Note.ID;
                        noticeVM.Type = Note.Type;

                        noticeListVM.Add(noticeVM);
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { notice = noticeListVM, messageCode = messageData };
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

        #region AddNotice
        [HttpPost]
        public IHttpActionResult AddNotice(NoticeViewModel noticeView)
        {
            try
            {
                string errorMessage = String.Empty;
                Notice notice = new Notice();
                if (notice != null)
                {
                    notice.Content = noticeView.Content;
                    notice.NoticeCode = noticeView.NoticeCode;
                    notice.Type = noticeView.Type;
                    notice.CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                    if (noticeView.NoticeMember != null)
                    {
                        notice.MemberNotices = new List<MemberNotice>();
                        MemberNotice note = new MemberNotice();
                        note.MemberId = noticeView.NoticeMember.MemberId;
                        note.NoticeId = notice.ID;
                        note.IsOpened = false;
                        note.IsSent = false;
                        notice.MemberNotices.Add(note);
                    }
                    _notice.Add(notice,out errorMessage);

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
    }
}
