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
        private readonly IEmployeeService _employee;
        private readonly IEventLogService _eventLog;

        public MemberController(IEmployeeService employee, IEventLogService eventLog)
        {
            _employee = employee;
            _eventLog = eventLog;
        }
        #region GetEmployee
        [HttpGet]
        public IHttpActionResult GetEmployee(int Id)
        {
            try
            {
                EmployeeViewModel employeeView = new EmployeeViewModel();
                Member employee = new Member();
                employee = _employee.GetEmployee(Id);
                if (employee != null)
                {
                    employeeView.id = employee.ID;
                    employeeView.userID = employee.UserID.Value;
                    employeeView.fullName = employee.User.FirstName + " " + employee.User.LastName;
                    employeeView.currentAddress = employee.User.CUrrentAddress;
                    employeeView.privateAddress = employee.User.PrivateAddress;
                    employeeView.nic = employee.User.NIC;
                    employeeView.dob = employee.User.DOB.ToString("yyyy-MM-dd");
                    employeeView.trainingCertificateNo = employee.TrainingCertificateNo;
                    employeeView.trainingCenter = employee.TrainingCenter;
                    employeeView.licenceNo = employee.LicenceNo;
                    employeeView.issuedDate = employee.IssuedDate.ToString("yyyy-MM-dd");
                    employeeView.expireDate = employee.ExpireDate.ToString("yyyy-MM-dd");
                    employeeView.highestEducation = employee.HighestEducation;

                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { item = employeeView, messageCode = messageData };
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

        #region GetEmployee
        [HttpGet]
        public IHttpActionResult AddEmployee(EmployeeViewModel employer)
        {
            try
            {
                Member employee = new Member();
                if (employer != null)
                {
                    //employeeView.id = employee.ID;
                    //employeeView.userID = employee.UserID;
                    //employeeView.fullName = employee.User.FirstName + " " + employee.User.LastName;
                    //employeeView.currentAddress = employee.User.CUrrentAddress;
                    //employeeView.privateAddress = employee.User.PrivateAddress;
                    //employeeView.nic = employee.User.NIC;
                    //employeeView.dob = employee.User.DOB.ToString("yyyy-MM-dd");
                    //employeeView.trainingCertificateNo = employee.TrainingCertificateNo;
                    //employeeView.trainingCenter = employee.TrainingCenter;
                    //employeeView.licenceNo = employee.LicenceNo;
                    //employeeView.issuedDate = employee.IssuedDate.ToString("yyyy-MM-dd");
                    //employeeView.expireDate = employee.ExpireDate.ToString("yyyy-MM-dd");
                    //employeeView.highestEducation = employee.HighestEducation;

                }


                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { item = "", messageCode = messageData };
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
