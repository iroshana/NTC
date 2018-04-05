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
    }
}
