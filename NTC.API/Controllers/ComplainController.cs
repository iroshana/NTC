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
    public class ComplainController : ApiController
    {
        private readonly IComplainService _complain;
        private readonly IEventLogService _eventLog;

        public ComplainController(IEventLogService eventLog, IComplainService complain)
        {
            _complain = complain;
            _eventLog = eventLog;
        }

        #region GetComplainByNo
        [HttpGet]
        public IHttpActionResult GetComplainByNo(string complainNo,int userId)
        {
            try
            {
                ComplainViewModel complainView = new ComplainViewModel();
                Complain complain = new Complain();
                complain = _complain.GetComplainByNo(complainNo, userId);
                if (complain != null)
                {
                    complainView.id = complain.ID;
                    complainView.complainNo = complain.ComplainNo;
                    complainView.description = complain.Description;
                    complainView.Category = new List<CategoryViewModel>();
                    foreach (ComplainCategory category in complain.ComplainCategories)
                    {
                        CategoryViewModel complainCategory = new CategoryViewModel();
                        complainCategory.categoryNo = category.Category.CategoryNo;
                        complainCategory.description = category.Category.Description;
                        complainView.Category.Add(complainCategory);
                    }
                }
                
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complain = complainView, messageCode = messageData };
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

        #region GetComplainByNo
        [HttpPost]
        public IHttpActionResult AddComplain(ComplainViewModel complainView)
        {
            try
            {
                Complain complain = new Complain();
                if (complain != null)
                {
                    complain.ID = complainView.id;
                    complain.ComplainNo = complainView.complainNo;
                    complain.BusId = complainView.bus.id;
                    complain.RouteId = complainView.route.id;
                    complain.Place = complainView.place;
                    complain.Date = DateTime.Parse(complainView.time);
                    complain.UserId = complainView.userId;
                    complain.Method = complainView.method;
                    complain.IsInqueryParticipation = complainView.isInqueryParticipation;
                    complain.IsEvidenceHave = complainView.isEvidenceHave;
                    complain.Description = complain.Description;
                    complain.ComplainCategories = new List<ComplainCategory>();
                    complain.Evidence = new Evidence();
                    if (complain.IsEvidenceHave)
                    {
                        complain.Evidence.FileName = complainView.evidence.fileName;
                        complain.Evidence.EvidenceNo = complainView.evidence.evidenceNo;
                        complain.Evidence.Extension = complainView.evidence.extension;
                        complain.Evidence.FilePath = complainView.evidence.filePath;
                    }

                    foreach (ComplainCategoryViewModel complainCategory in complainView.complainCategory)
                    {
                        ComplainCategory category = new ComplainCategory();
                        category.CategoryId = complainCategory.categoryId;
                        category.ComplainId = complain.ID;
                        category.Description = String.IsNullOrEmpty(complainCategory.description)?String.Empty: complainCategory.description;

                        complain.ComplainCategories.Add(category);
                    }
                    
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complain = complainView, messageCode = messageData };
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
