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
        private readonly IMemberService _member;

        public ComplainController(IEventLogService eventLog, IComplainService complain, ICommonDataService commonData, IMemberService member)
        {
            _complain = complain;
            _eventLog = eventLog;
            _commonData = commonData;
            _member = member;
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
                    complainView.bus = new BusViewModel();
                    complainView.bus.id = complain.BusId;
                    complainView.bus.busNo = complain.Bus.LicenceNo;
                    complainView.route = new RouteViewModel();
                    complainView.route.id = complain.RouteId;
                    complainView.route.from = complain.Route.From;
                    complainView.route.to = complain.Route.To;
                    complainView.route.routeNo = complain.Route.RouteNo;
                    complainView.place = String.IsNullOrEmpty(complain.Place) ? String.Empty : complain.Place;
                    complainView.complainDate = complain.Date.ToString(@"yyyy-MM-dd");
                    complainView.userId = complain.UserId == null ? 0 : complain.UserId.Value;
                    complainView.method = String.IsNullOrEmpty(complain.Method) ? String.Empty : complain.Method;
                    complainView.isInqueryParticipation = complain.IsInqueryParticipation;
                    complainView.isEvidenceHave = complain.IsEvidenceHave;
                    complainView.description = String.IsNullOrEmpty(complain.Description) ? String.Empty : complain.Description;
                    complainView.complainerName = String.IsNullOrEmpty(complain.ComplainerName) ? String.Empty : complain.ComplainerName;
                    complainView.complainerAddress = String.IsNullOrEmpty(complain.ComplainerAddress) ? String.Empty : complain.ComplainerAddress;
                    complainView.status = complain.ComplainStatus;
                    complainView.telNo = String.IsNullOrEmpty(complain.ComplainerTel) ? String.Empty : complain.ComplainerTel;


                    if (complain.IsEvidenceHave)
                    {
                        complainView.evidence = new EvidenceViewModel();
                        complainView.evidence.fileName = String.IsNullOrEmpty(complain.Evidence.FileName) ? String.Empty : complainView.evidence.fileName;
                        complainView.evidence.evidenceNo = String.IsNullOrEmpty(complain.Evidence.EvidenceNo) ? String.Empty : complainView.evidence.evidenceNo;
                        complainView.evidence.extension = String.IsNullOrEmpty(complain.Evidence.Extension) ? String.Empty : complainView.evidence.extension;
                        complainView.evidence.filePath = String.IsNullOrEmpty(complain.Evidence.FilePath) ? String.Empty : complainView.evidence.filePath;
                        
                    }

                    complainView.Category = new List<CategoryViewModel>();
                    
                    foreach (ComplainCategory category in complain.ComplainCategories)
                    {
                        if (category.IsSelected)
                        {
                            CategoryViewModel complainCategory = new CategoryViewModel();
                            complainCategory.id = category.ComplainId;
                            complainCategory.categoryNo = String.IsNullOrEmpty(category.Category.CategoryNo) ? String.Empty : category.Category.CategoryNo;
                            complainCategory.description = String.IsNullOrEmpty(category.Category.Description) ? String.Empty : category.Category.Description;
                            complainCategory.isSelected = category.IsSelected;
                            complainView.Category.Add(complainCategory);
                        }                        
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

        #region Add Complain
        [HttpPost]
        public IHttpActionResult AddComplain(ComplainDataViewModel complainView)
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
                    complain.Date = complainView.complainDate;
                    complain.ComplainStatus = "Filed";
                    complain.UserId = complainView.userId == 0 ? (int?)null : complainView.userId;
                    if (complainView.memberId != 0) { 
                        Member member = _member.GetMember(complainView.memberId);
                        if (member.MemberType.Code == "Driver")
                        {
                            complain.DriverId = member.ID;
                            complain.ConductorId = (int?)null;
                        }
                        else
                        {
                            complain.DriverId = (int?)null;
                            complain.ConductorId = member.ID;
                        }
                    }
                    else
                    {
                        complain.DriverId = _commonData.GetBusById(complainView.bus.id).DriverId;
                        complain.ConductorId = _commonData.GetBusById(complainView.bus.id).ConductorId;
                    }
                    complain.Method = String.IsNullOrEmpty(complainView.method) ? String.Empty : complainView.method;
                    complain.IsInqueryParticipation = complainView.isInqueryParticipation;
                    complain.IsEvidenceHave = complainView.isEvidenceHave;
                    complain.Description = String.IsNullOrEmpty(complainView.description) ? String.Empty : complainView.description;
                    complain.ComplainerName = String.IsNullOrEmpty(complainView.complainerName) ? String.Empty : complainView.complainerName;
                    complain.ComplainerAddress = String.IsNullOrEmpty(complainView.complainerAddress) ? String.Empty : complainView.complainerAddress;
                    complain.ComplainerTel = String.IsNullOrEmpty(complainView.telNo) ? String.Empty : complainView.telNo;

                    complain.ComplainCategories = new List<ComplainCategory>();
                    
                    if (complain.IsEvidenceHave)
                    {
                        complain.Evidence = new Evidence();
                        complain.Evidence.FileName = String.IsNullOrEmpty(complainView.evidence.fileName) ? String.Empty : complainView.evidence.fileName;
                        complain.Evidence.EvidenceNo = complainView.complainNo + complainView.evidence.fileName;
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
                        category.IsSelected = complainCategory.isSelected;

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
                BusViewModel busViewModel = new BusViewModel();
                Bus buses = new Bus();
                buses = _commonData.SearchBuses(busNo).FirstOrDefault();
                if (buses != null)
                {                   
                    busViewModel.id = buses.ID;
                    busViewModel.busNo = buses.LicenceNo;
                    busViewModel.route = new RouteViewModel();
                    busViewModel.route.id = buses.Route.ID;
                    busViewModel.route.routeNo = buses.Route.RouteNo;
                    busViewModel.route.from = buses.Route.From;
                    busViewModel.route.to = buses.Route.To;                    
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

        [HttpGet]
        public IHttpActionResult GetAllComplainsByMember(int memberId)
        {
            try
            {
                List<CategoryViewModel> categoryList = new List<CategoryViewModel>();
                IEnumerable<Complain> complains = new List<Complain>();
                complains = _complain.GetAllComplains(memberId);
                if (complains != null)
                {
                    foreach (Complain complain in complains)
                    {
                        foreach (ComplainCategory category in complain.ComplainCategories)
                        {
                            if (categoryList.Find(x=>x.categoryNo == category.Category.CategoryNo) == null && category.IsSelected == true)
                            {
                                CategoryViewModel complainCategory = new CategoryViewModel();
                                complainCategory.id = category.ComplainId;
                                complainCategory.categoryNo = String.IsNullOrEmpty(category.Category.CategoryNo) ? String.Empty : category.Category.CategoryNo;
                                complainCategory.description = String.IsNullOrEmpty(category.Category.Description) ? String.Empty : category.Category.Description;

                                categoryList.Add(complainCategory);
                            }
                        }
                    }
                }

                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainsCategory = categoryList, messageCode = messageData };
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

        #region ChangeComplainStatus
        [HttpPost]
        public IHttpActionResult ChangeComplainStatus(int complainId,string status)
        {
            try
            {
                string errorMessage = String.Empty;
                Complain complain = new Complain();
                complain = _complain.GetAll(x=>x.ID == complainId).FirstOrDefault();
                complain.ComplainStatus = status;

                _complain.Update(complain);

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
