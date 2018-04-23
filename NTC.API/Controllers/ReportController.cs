using NTC.BusinessEntities;
using NTC.InterfaceServices;
using NTC.ViewModels;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;

namespace NTC.API.Controllers
{
    public class ReportController : ApiController
    {
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IReportService _report;
        private readonly IComplainService _complain;
        private readonly IExelReportService _exel;
        public ReportController(IEventLogService eventLog, ICommonDataService common, IReportService report,IComplainService complain, IExelReportService exel)
        {
            _eventLog = eventLog;
            _common = common;
            _report = report;
            _complain = complain;
            _exel = exel;
        }

        #region GetDemeritReports
        [HttpPost]
        public IHttpActionResult GetDemeritReports(MeritReportDateModel search)
        {
            try
            {
                List<MeritReportViewModel> meritList = new List<MeritReportViewModel>();
                IEnumerable<MeritReportEntityModel> merits = new List<MeritReportEntityModel>();
                merits = _report.CreateReport(search.colorCodeId, search.fromDate, search.toDate, search.typeId, search.order);

                if (merits.Count() > 0)
                {
                    foreach (MeritReportEntityModel merit in merits)
                    {
                        MeritReportViewModel meritView = new MeritReportViewModel();
                        meritView.id = merit.ID;
                        meritView.fullName = merit.FullName;
                        meritView.description = merit.Description;
                        meritView.inqueryDate = merit.InqueryDate.ToString(@"yyyy-MM-dd");
                        meritView.ntcNo = merit.NTCNo;
                        meritList.Add(meritView);
                    }
                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { roleList = meritList, messageCode = messageData };
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

        #region GetComplainReport
        [HttpGet]
        public IHttpActionResult GetComplainReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<ComplainReportViewModel> complainList = new List<ComplainReportViewModel>();
                IEnumerable<Complain> complains = new List<Complain>();
                complains = _complain.GetAllComplains(fromDate,toDate) ;

                if (complains.Count() > 0)
                {
                    foreach (Complain complain in complains)
                    {
                        foreach (ComplainCategory category in complain.ComplainCategories)
                        {
                            if (category.IsSelected == true)
                            {
                                ComplainReportViewModel complainView = new ComplainReportViewModel();
                                complainView.driverName = complain.Member.FullName;
                                complainView.ntcNo = complain.Member.NTCNo;
                                complainView.complain = String.IsNullOrEmpty(category.Category.Description) ? String.Empty : category.Category.Description;
                                complainView.complainStatus = complain.ComplainStatus;

                                complainList.Add(complainView);
                            }
                            
                        }
                        
                    }
                }
                
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { complainList = complainList, messageCode = messageData };
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

        #region GetComplainReport
        [HttpGet]
        public HttpResponseMessage ExportAdvisingPannelReport(DateTime fromDate, DateTime toDate,int typeId,string order)
        {
            try
            {
                List<MeritReportViewModel> meritList = new List<MeritReportViewModel>();
                IEnumerable<MeritReportEntityModel> merits = new List<MeritReportEntityModel>();
                merits = _report.CreateReport(2, fromDate, toDate, typeId, order);

                if (merits.Count() > 0)
                {
                    foreach (MeritReportEntityModel merit in merits)
                    {
                        MeritReportViewModel meritView = new MeritReportViewModel();
                        meritView.id = merit.ID;
                        meritView.fullName = merit.FullName;
                        meritView.description = merit.Description;
                        meritView.inqueryDate = merit.InqueryDate.ToString(@"yyyy-MM-dd");
                        meritView.ntcNo = merit.NTCNo;
                        meritList.Add(meritView);
                    }
                }
                MemoryStream stream = new MemoryStream();
                HttpResponseMessage result = new HttpResponseMessage();
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report - " + DateTime.Now.ToShortDateString());

                    // Add some formatting to the worksheet
                    worksheet.TabColor = System.Drawing.Color.Blue;
                    worksheet.DefaultRowHeight = 12;
                    worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
                    worksheet.Row(1).Height = 20;
                    worksheet.Row(2).Height = 18;

                    // Start adding the header
                    _exel.CreateExcelHeader(worksheet, "Addvising Pannel Report", 1, 2, "A1", "H1", false, true);
                    _exel.CreateExcelHeader(worksheet, "NTC NO", 3, 1, "A4", "B4", true, true);
                    _exel.CreateExcelHeader(worksheet, "Driver Name", 3, 2, "C4", "D4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Offence", 3, 3, "E4", "F4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Pannel Date", 3, 4, "G4", "H4", false, true);
                    

                    int rowId = 5;
                    int receiptCount = meritList.Count();
                    foreach (var s in meritList)
                    {
                        _exel.CreateExcelContents(worksheet, s.ntcNo, rowId, 1, "A" + rowId, "B" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.fullName, rowId, 2, "C" + rowId, "D" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.description, rowId, 3, "E" + rowId, "F" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.inqueryDate, rowId, 4, "G" + rowId, "H" + rowId, true, true, false, false, "", "");
                       
                        rowId += 1;
                    }
                    package.Workbook.Properties.Title = "ADVISING PANNEL REPORT";
                    package.Workbook.Properties.Author = "NTC";
                    package.Workbook.Properties.Company = "NTC";

                    result.Content = new ByteArrayContent(package.GetAsByteArray());


                }
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Advising Pannel Report - " + DateTime.Now.ToShortDateString() + ".xlsx"
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetComplainReport
        [HttpGet]
        public HttpResponseMessage FinePayReport(DateTime fromDate, DateTime toDate, int typeId, string order)
        {
            try
            {
                List<MeritReportViewModel> meritList = new List<MeritReportViewModel>();
                IEnumerable<MeritReportEntityModel> merits = new List<MeritReportEntityModel>();
                merits = _report.CreateReport(4, fromDate, toDate, typeId, order);

                if (merits.Count() > 0)
                {
                    foreach (MeritReportEntityModel merit in merits)
                    {
                        MeritReportViewModel meritView = new MeritReportViewModel();
                        meritView.id = merit.ID;
                        meritView.fullName = merit.FullName;
                        meritView.description = merit.Description;
                        meritView.inqueryDate = merit.InqueryDate.ToString(@"yyyy-MM-dd");
                        meritView.ntcNo = merit.NTCNo;
                        meritList.Add(meritView);
                    }
                }
                MemoryStream stream = new MemoryStream();
                HttpResponseMessage result = new HttpResponseMessage();
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report - " + DateTime.Now.ToShortDateString());

                    // Add some formatting to the worksheet
                    worksheet.TabColor = System.Drawing.Color.Blue;
                    worksheet.DefaultRowHeight = 12;
                    worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
                    worksheet.Row(1).Height = 20;
                    worksheet.Row(2).Height = 18;

                    // Start adding the header
                    _exel.CreateExcelHeader(worksheet, "Fine Payment Report", 1, 2, "A1", "H1", false, true);
                    _exel.CreateExcelHeader(worksheet, "NTC NO", 3, 1, "A4", "B4", true, true);
                    _exel.CreateExcelHeader(worksheet, "Driver Name", 3, 2, "C4", "D4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Offence", 3, 3, "E4", "F4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Fine Payment Date", 3, 4, "G4", "H4", false, true);


                    int rowId = 5;
                    int receiptCount = meritList.Count();
                    foreach (var s in meritList)
                    {
                        _exel.CreateExcelContents(worksheet, s.ntcNo, rowId, 1, "A" + rowId, "B" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.fullName, rowId, 2, "C" + rowId, "D" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.description, rowId, 3, "E" + rowId, "F" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.inqueryDate, rowId, 4, "G" + rowId, "H" + rowId, true, true, false, false, "", "");

                        rowId += 1;
                    }
                    package.Workbook.Properties.Title = "FINE PAY REPORT";
                    package.Workbook.Properties.Author = "NTC";
                    package.Workbook.Properties.Company = "NTC";

                    result.Content = new ByteArrayContent(package.GetAsByteArray());


                }
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Fine Pay Report - " + DateTime.Now.ToShortDateString() + ".xlsx"
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ExportPunishmentTrainingReport
        [HttpGet]
        public HttpResponseMessage ExportPunishmentTrainingReport(DateTime fromDate, DateTime toDate, int typeId, string order)
        {
            try
            {
                List<MeritReportViewModel> meritList = new List<MeritReportViewModel>();
                IEnumerable<MeritReportEntityModel> merits = new List<MeritReportEntityModel>();
                merits = _report.CreateReport(3, fromDate, toDate, typeId, order);

                if (merits.Count() > 0)
                {
                    foreach (MeritReportEntityModel merit in merits)
                    {
                        MeritReportViewModel meritView = new MeritReportViewModel();
                        meritView.id = merit.ID;
                        meritView.fullName = merit.FullName;
                        meritView.description = merit.Description;
                        meritView.inqueryDate = merit.InqueryDate.ToString(@"yyyy-MM-dd");
                        meritView.ntcNo = merit.NTCNo;
                        meritList.Add(meritView);
                    }
                }
                MemoryStream stream = new MemoryStream();
                HttpResponseMessage result = new HttpResponseMessage();
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report - " + DateTime.Now.ToShortDateString());

                    // Add some formatting to the worksheet
                    worksheet.TabColor = System.Drawing.Color.Blue;
                    worksheet.DefaultRowHeight = 12;
                    worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
                    worksheet.Row(1).Height = 20;
                    worksheet.Row(2).Height = 18;

                    // Start adding the header
                    _exel.CreateExcelHeader(worksheet, "Punishment Training Report", 1, 2, "A1", "H1", false, true);
                    _exel.CreateExcelHeader(worksheet, "NTC NO", 3, 1, "A4", "B4", true, true);
                    _exel.CreateExcelHeader(worksheet, "Driver Name", 3, 2, "C4", "D4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Offence", 3, 3, "E4", "F4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Punishment Training Date", 3, 4, "G4", "H4", false, true);


                    int rowId = 5;
                    int receiptCount = meritList.Count();
                    foreach (var s in meritList)
                    {
                        _exel.CreateExcelContents(worksheet, s.ntcNo, rowId, 1, "A" + rowId, "B" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.fullName, rowId, 2, "C" + rowId, "D" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.description, rowId, 3, "E" + rowId, "F" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.inqueryDate, rowId, 4, "G" + rowId, "H" + rowId, true, true, false, false, "", "");

                        rowId += 1;
                    }
                    package.Workbook.Properties.Title = "PUNISHMENT TRAINING REPORT";
                    package.Workbook.Properties.Author = "NTC";
                    package.Workbook.Properties.Company = "NTC";

                    result.Content = new ByteArrayContent(package.GetAsByteArray());


                }
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Punishment Training Report - " + DateTime.Now.ToShortDateString() + ".xlsx"
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region GetComplainReport
        [HttpGet]
        public HttpResponseMessage CancelationReport(DateTime fromDate, DateTime toDate, int typeId, string order)
        {
            try
            {
                List<MeritReportViewModel> meritList = new List<MeritReportViewModel>();
                IEnumerable<MeritReportEntityModel> merits = new List<MeritReportEntityModel>();
                merits = _report.CreateReport(1, fromDate, toDate, typeId, order);

                if (merits.Count() > 0)
                {
                    foreach (MeritReportEntityModel merit in merits)
                    {
                        MeritReportViewModel meritView = new MeritReportViewModel();
                        meritView.id = merit.ID;
                        meritView.fullName = merit.FullName;
                        meritView.description = merit.Description;
                        meritView.inqueryDate = merit.InqueryDate.ToString(@"yyyy-MM-dd");
                        meritView.ntcNo = merit.NTCNo;
                        meritList.Add(meritView);
                    }
                }
                MemoryStream stream = new MemoryStream();
                HttpResponseMessage result = new HttpResponseMessage();
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report - " + DateTime.Now.ToShortDateString());

                    // Add some formatting to the worksheet
                    worksheet.TabColor = System.Drawing.Color.Blue;
                    worksheet.DefaultRowHeight = 12;
                    worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
                    worksheet.Row(1).Height = 20;
                    worksheet.Row(2).Height = 18;

                    // Start adding the header
                    _exel.CreateExcelHeader(worksheet, "Cancelation Of License Report", 1, 2, "A1", "H1", false, true);
                    _exel.CreateExcelHeader(worksheet, "NTC NO", 3, 1, "A4", "B4", true, true);
                    _exel.CreateExcelHeader(worksheet, "Driver Name", 3, 2, "C4", "D4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Offence", 3, 3, "E4", "F4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Pannel Date", 3, 4, "G4", "H4", false, true);


                    int rowId = 5;
                    int receiptCount = meritList.Count();
                    foreach (var s in meritList)
                    {
                        _exel.CreateExcelContents(worksheet, s.ntcNo, rowId, 1, "A" + rowId, "B" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.fullName, rowId, 2, "C" + rowId, "D" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.description, rowId, 3, "E" + rowId, "F" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.inqueryDate, rowId, 4, "G" + rowId, "H" + rowId, true, true, false, false, "", "");

                        rowId += 1;
                    }
                    package.Workbook.Properties.Title = "CANCELATION OF LICENSE REPORT";
                    package.Workbook.Properties.Author = "NTC";
                    package.Workbook.Properties.Company = "NTC";

                    result.Content = new ByteArrayContent(package.GetAsByteArray());


                }
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Cancelation Of License Report - " + DateTime.Now.ToShortDateString() + ".xlsx"
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region ComplainManagementReport
        [HttpGet]
        public HttpResponseMessage ComplainManagementReport(DateTime fromDate, DateTime toDate)
        {
            try
            {
                List<ComplainReportViewModel> complainList = new List<ComplainReportViewModel>();
                IEnumerable<Complain> complains = new List<Complain>();
                complains = _complain.GetAllComplains(fromDate, toDate);

                if (complains.Count() > 0)
                {
                    foreach (Complain complain in complains)
                    {
                        foreach (ComplainCategory category in complain.ComplainCategories)
                        {
                            if (category.IsSelected)
                            {
                                ComplainReportViewModel complainView = new ComplainReportViewModel();
                                complainView.driverName = complain.Member.FullName;
                                complainView.ntcNo = complain.Member.NTCNo;
                                complainView.complain = String.IsNullOrEmpty(category.Category.Description) ? String.Empty : category.Category.Description;
                                complainView.complainStatus = String.IsNullOrEmpty(complain.ComplainStatus) ? String.Empty : complain.ComplainStatus;

                                complainList.Add(complainView);
                            }
                            
                        }
                        
                    }
                }
                MemoryStream stream = new MemoryStream();
                HttpResponseMessage result = new HttpResponseMessage();
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Report - " + DateTime.Now.ToShortDateString());

                    // Add some formatting to the worksheet
                    worksheet.TabColor = System.Drawing.Color.Blue;
                    worksheet.DefaultRowHeight = 12;
                    worksheet.HeaderFooter.FirstFooter.LeftAlignedText = string.Format("Generated: {0}", DateTime.Now.ToShortDateString());
                    worksheet.Row(1).Height = 20;
                    worksheet.Row(2).Height = 18;

                    // Start adding the header
                    _exel.CreateExcelHeader(worksheet, "Complain Management Report", 1, 2, "A1", "H1", false, true);
                    _exel.CreateExcelHeader(worksheet, "NTC NO", 3, 1, "A4", "B4", true, true);
                    _exel.CreateExcelHeader(worksheet, "Driver Name", 3, 2, "C4", "D4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Complain", 3, 3, "E4", "F4", false, true);
                    _exel.CreateExcelHeader(worksheet, "Status", 3, 4, "G4", "H4", false, true);


                    int rowId = 5;
                    int receiptCount = complainList.Count();
                    foreach (var s in complainList)
                    {
                        _exel.CreateExcelContents(worksheet, s.ntcNo, rowId, 1, "A" + rowId, "B" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.driverName, rowId, 2, "C" + rowId, "D" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.complain, rowId, 3, "E" + rowId, "F" + rowId, true, true, false, false, "", "");
                        _exel.CreateExcelContents(worksheet, s.complainStatus, rowId, 4, "G" + rowId, "H" + rowId, true, true, false, false, "", "");

                        rowId += 1;
                    }
                    package.Workbook.Properties.Title =  "COMPLAIN MANAGEMENT REPORT";
                    package.Workbook.Properties.Author = "NTC";
                    package.Workbook.Properties.Company = "NTC";

                    result.Content = new ByteArrayContent(package.GetAsByteArray());


                }
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Complain Management Report - " + DateTime.Now.ToShortDateString() + ".xlsx"
                };
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
