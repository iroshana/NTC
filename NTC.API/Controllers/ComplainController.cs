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
    public class ComplainController : ApiController
    {
        private readonly IComplainService _complain;
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _commonData;


        public ComplainController(IEventLogService eventLog, IComplainService complain, ICommonDataService commonData)
        {
            _complain = complain;
            _eventLog = eventLog;
            _commonData = commonData;
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
                    complainView.complainNo = String.IsNullOrEmpty(complain.ComplainNo) ? String.Empty : complain.ComplainNo;
                    complainView.description = String.IsNullOrEmpty(complain.Description) ? String.Empty : complain.Description;
                    complainView.Category = new List<CategoryViewModel>();
                    foreach (ComplainCategory category in complain.ComplainCategories)
                    {
                        CategoryViewModel complainCategory = new CategoryViewModel();
                        complainCategory.id = category.ComplainId;
                        complainCategory.categoryNo = String.IsNullOrEmpty(category.Category.CategoryNo) ? String.Empty : category.Category.CategoryNo;
                        complainCategory.description = String.IsNullOrEmpty(category.Category.Description) ? String.Empty : category.Category.Description;
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
                string errorMessage = String.Empty;
                Complain complain = new Complain();
                if (complain != null)
                {
                    complain.ID = complainView.id;
                    complain.ComplainNo = String.IsNullOrEmpty(complainView.complainNo) ? String.Empty : complainView.complainNo;
                    complain.BusId = complainView.bus.id;
                    complain.RouteId = complainView.route.id;
                    complain.Place = String.IsNullOrEmpty(complainView.place) ? String.Empty : complainView.place;
                    complain.Date = DateTime.Parse(complainView.time);
                    complain.UserId = complainView.userId == 0 ? (int?)null : complainView.userId;
                    complain.MemberId = complainView.memberId;
                    complain.Method = String.IsNullOrEmpty(complainView.method) ? String.Empty : complainView.method;
                    complain.IsInqueryParticipation = complainView.isInqueryParticipation;
                    complain.IsEvidenceHave = complainView.isEvidenceHave;
                    complain.Description = String.IsNullOrEmpty(complain.Description) ? String.Empty : complain.Description;
                    complain.ComplainCategories = new List<ComplainCategory>();
                    
                    if (complain.IsEvidenceHave)
                    {
                        complain.Evidence = new Evidence();
                        complain.Evidence.FileName = String.IsNullOrEmpty(complainView.evidence.fileName) ? String.Empty : complainView.evidence.fileName;
                        complain.Evidence.EvidenceNo = String.IsNullOrEmpty(complainView.evidence.evidenceNo) ? String.Empty : complainView.evidence.evidenceNo;
                        complain.Evidence.Extension = String.IsNullOrEmpty(complainView.evidence.extension) ? String.Empty : complainView.evidence.extension;
                        complain.Evidence.FilePath = String.IsNullOrEmpty(complainView.evidence.filePath) ? String.Empty : complainView.evidence.filePath;
                        complain.Evidence.CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                        complain.EvidenceId = complain.Evidence.ID;
                        
                    }
                    
                    foreach (CategoryViewModel complainCategory in complainView.Category)
                    {
                        ComplainCategory category = new ComplainCategory();
                        category.CategoryId = complainCategory.id;
                        category.ComplainId = complain.ID;
                        category.Description = String.IsNullOrEmpty(complainCategory.description)?String.Empty: complainCategory.description;

                        complain.ComplainCategories.Add(category);
                    }
                    _complain.Add(complain,out errorMessage);
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

        #region SearchBus
        [HttpGet]
        public IHttpActionResult SearchBus(string busNo)
        {
            try
            {
                List<BusViewModel> busViewModel = new List<BusViewModel>();
                IEnumerable<Bus> buses = new List<Bus>();
                buses = _commonData.SearchBuses(busNo);
                if (buses != null)
                {
                    foreach (Bus bus in buses)
                    {
                        BusViewModel busVM = new BusViewModel();
                        busVM.id = bus.ID;
                        busVM.busNo = bus.LicenceNo;
                        busVM.route = new RouteViewModel();
                        if (bus.Route != null)
                        {
                            busVM.route.id = bus.Route.ID;
                            busVM.route.routeNo = bus.Route.RouteNo;
                            busVM.route.from = bus.Route.From;
                            busVM.route.to = bus.Route.To;
                        }

                        busViewModel.Add(busVM);
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { bus = busViewModel, messageCode = messageData };
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

        #region GetBusByNo
        [HttpGet]
        public IHttpActionResult GetBusByNo(string busNo)
        {
            try
            {
                BusViewModel busVM = new BusViewModel();
                Bus bus = new Bus();
                bus = _commonData.GetBusByNo(busNo);
                if (bus != null)
                {
                    busVM.id = bus.ID;
                    busVM.busNo = bus.LicenceNo;
                    busVM.route = new RouteViewModel();
                    if (bus.Route != null)
                    {
                        busVM.route.id = bus.Route.ID;
                        busVM.route.routeNo = bus.Route.RouteNo;
                        busVM.route.from = bus.Route.From;
                        busVM.route.to = bus.Route.To;
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { bus = busVM, messageCode = messageData };
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

        #region GetAllCategorties
        [HttpGet]
        public IHttpActionResult GetAllCategorties()
        {
            try
            {
                List<CategoryViewModel> categoryVM = new List<CategoryViewModel>();
                IEnumerable<Category> category = new List<Category>();

                category = _commonData.GetAllCategories();

                if (category.Count()>0)
                {
                    foreach (Category cat in category)
                    {
                        CategoryViewModel categoryView = new CategoryViewModel();
                        categoryView.id = cat.ID;
                        categoryView.categoryNo = cat.CategoryNo;
                        categoryView.description = cat.Description;
                        categoryView.isSelected = false;

                        categoryVM.Add(categoryView);
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { categories = categoryVM, messageCode = messageData };
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

        #region GetLastComplainNo
        [HttpGet]
        public IHttpActionResult GetLastComplainNo()
        {
            try
            {
                Complain complain = new Complain();
                complain = _complain.GetLastComplain();
                DateTime date = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));


                string today = date.ToShortDateString().Replace("/", String.Empty);
                string nextComplainNo = String.Empty;

                if (complain != null)
                {
                    string complainNo = complain.ComplainNo;

                    string complaindate = complainNo.Split('-')[0];
                    int no = int.Parse(complainNo.Split('-')[1]);

                    if (complaindate == today)
                    {
                        nextComplainNo = complaindate + "-" + (no + 1);
                    }
                    else
                    {
                        nextComplainNo = today + "-" + "1";
                    }
                }else
                {
                    nextComplainNo = today + "-" + "1";
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainNo = nextComplainNo, messageCode = messageData };
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

        #region GetComplainNo
        [HttpGet]
        public IHttpActionResult GetComplainNo(int userId)
        {
            try
            {
                IEnumerable<Complain> complain = new List<Complain>();
                complain = _complain.GetComplainNo(userId);

                List<string> complainNos = complain.Select(x => x.ComplainNo).ToList();

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainNo = complainNos, messageCode = messageData };
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
