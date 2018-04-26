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
        private readonly IMemberNoticeService _memberNotice;
        private readonly IMemberService _member;
        public NoticeController(IMemberNoticeService memberNotice, INoticeService notice, IEventLogService eventLog, ICommonDataService common, IMemberService member)
        {
            _notice = notice;
            _eventLog = eventLog;
            _common = common;
            _memberNotice = memberNotice;
            _member = member;
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
                        if (!Note.IsSent)
                        {
                            NoticeViewModel noticeVM = new NoticeViewModel();
                            noticeVM.Content = Note.Content;
                            noticeVM.NoticeCode = Note.NoticeCode;
                            noticeVM.ID = Note.ID;
                            noticeVM.Type = Note.Type;

                            noticeListVM.Add(noticeVM);
                        }

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
                    notice.IsSent = false;
                    notice.CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                    notice.IsGeneratNotice = noticeView.memberId == 0 ? true : false;

                    if (noticeView.memberId != 0)
                    {
                        notice.MemberNotices = new List<MemberNotice>();
                        MemberNotice note = new MemberNotice();
                        note.MemberId = noticeView.memberId;
                        note.NoticeId = notice.ID;
                        note.IsOpened = false;
                        note.IsSent = false;
                        notice.MemberNotices.Add(note);
                    }
                    notice = _notice.Add(notice, out errorMessage);

                }
                var messageData = new
                {
                    code = String.IsNullOrEmpty(errorMessage) ? Constant.SuccessMessageCode : Constant.ErrorMessageCode
                   ,
                    message = String.IsNullOrEmpty(errorMessage) ? Constant.MessageSuccess : errorMessage
                };
                var returnObject = new { messageCode = messageData, noticeId = notice.ID };
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

        #region AddBulkNotice
        [HttpPost]
        public IHttpActionResult AddBulkNotice(BulkNoticeViewModel noticeView)
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
                    notice.IsSent = true;
                    notice.CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                    if (noticeView.members != null)
                    {
                        notice.MemberNotices = new List<MemberNotice>();
                        foreach (int Id in noticeView.members)
                        {
                            MemberNotice note = new MemberNotice();
                            note.MemberId = Id;
                            note.NoticeId = notice.ID;
                            note.IsOpened = false;
                            note.IsSent = false;
                            notice.MemberNotices.Add(note);
                        }
                    }
                    notice = _notice.Add(notice, out errorMessage);

                }
                var messageData = new
                {
                    code = String.IsNullOrEmpty(errorMessage) ? Constant.SuccessMessageCode : Constant.ErrorMessageCode
                   ,
                    message = String.IsNullOrEmpty(errorMessage) ? Constant.MessageSuccess : errorMessage
                };
                var returnObject = new { messageCode = messageData, noticeId = notice.ID };
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

        #region Get ALL Memeber Notice
        [HttpGet]
        public IHttpActionResult GetMemberNotice(int memberId)
        {
            try
            {
                List<NoticeViewModel> noticeListVM = new List<NoticeViewModel>();
                IEnumerable<MemberNotice> notice = new List<MemberNotice>();
                notice = _memberNotice.GetAllMemeberNotice(memberId);
                if (notice != null)
                {
                    foreach (MemberNotice note in notice)
                    {
                        if (!note.Notice.IsSent)
                        {
                            NoticeViewModel noticeVM = new NoticeViewModel();
                            noticeVM.Content = note.Notice.Content;
                            noticeVM.NoticeCode = note.Notice.NoticeCode;
                            noticeVM.ID = note.Notice.ID;
                            noticeVM.Type = note.Notice.Type;

                            noticeListVM.Add(noticeVM);
                        }

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

        #region SentMemberNotice
        [HttpGet]
        public IHttpActionResult SentMemberNotice(int noticeId)
        {
            try
            {
                string errorMessage = String.Empty;
                Notice notice = new Notice();
                notice = _notice.GetAll(x => x.ID == noticeId && x.IsGeneratNotice == true).FirstOrDefault();
                notice.IsSent = true;
                _notice.UpdaterNotice(notice, out errorMessage);

                MemberNotice noticemem = _memberNotice.GetAll(x => x.ID == noticeId).FirstOrDefault();
                noticemem.IsSent = true;
                _memberNotice.UpdateMemberNotice(noticemem, out errorMessage);

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
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

        #region SentNotice
        [HttpGet]
        public IHttpActionResult SentNotice(int noticeId)
        {
            try
            {
                string errorMessage = String.Empty;
                Notice notice = new Notice();
                notice = _notice.GetAll(x => x.ID == noticeId && x.IsGeneratNotice == true).FirstOrDefault();
                notice.IsSent = true;

                _notice.UpdaterNotice(notice, out errorMessage);
                IEnumerable<Member> members = _member.GetAll().ToList();
                foreach (Member member in members)
                {
                    MemberNotice noticeVM = new MemberNotice();
                    noticeVM.MemberId = member.ID;
                    noticeVM.NoticeId = notice.ID;
                    noticeVM.IsSent = true;
                    noticeVM.IsOpened = false;

                    _memberNotice.Add(noticeVM);
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
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
