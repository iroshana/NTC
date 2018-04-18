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
    public class UserController : ApiController
    {
        private readonly IEventLogService _eventLog;
        private readonly ICommonDataService _common;
        private readonly IUserService _user;

        public UserController(IEventLogService eventLog, ICommonDataService common, IUserService user)
        {
            _eventLog = eventLog;
            _common = common;
            _user = user;
        }

        #region GeRoles
        [HttpGet]
        public IHttpActionResult GetRoles()
        {
            try
            {
                List<RoleViewModel> roleList = new List<RoleViewModel>();
                IEnumerable<Role> roles =  _user.GetUserRoles();
                if (roles != null)
                {
                    foreach (Role role in roles)
                    {
                        RoleViewModel roleView = new RoleViewModel();
                        roleView.id = role.ID;
                        roleView.code = role.Code;
                        roleView.name = role.Name;

                        roleList.Add(roleView);
                    }
                }
                var messageData = new { code = Constant.SuccessMessageCode, message = Constant.MessageSuccess };
                var returnObject = new { roleList = roleList, messageCode = messageData };
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

        #region RegisterUser
        [HttpPost]
        public IHttpActionResult RegisterUser(UserViewModel userView)
        {
            try
            {
                string errorMessage = String.Empty;
                if (userView != null)
                {
                    User newUser = new User();
                    newUser.UserName = userView.userName;
                    newUser.Email = userView.email;
                    newUser.NIC = userView.nic;
                    newUser.TelNo = userView.telNo;
                    newUser.password = userView.password;

                    _user.registerUser(newUser, userView.roleId, userView.memberId, out errorMessage);
                }
                else
                {
                    errorMessage = Constant.MessageGeneralError;
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

        #region Login
        [HttpPost]
        public IHttpActionResult Login(UserViewModel userView)
        {
            try
            {
                string errorMessage = String.Empty;
                UserLoginViewModel userLogin = new UserLoginViewModel();
                if (userView != null)
                {
                    userLogin = _user.validateUser(userView.userName,userView.password,out errorMessage);
                }
                else
                {
                    errorMessage = Constant.MessageGeneralError;
                }

                var messageData = new
                {
                    code = String.IsNullOrEmpty(errorMessage) ? Constant.SuccessMessageCode : Constant.ErrorMessageCode
                   ,
                    message = String.IsNullOrEmpty(errorMessage) ? Constant.MessageSuccess : errorMessage
                };
                var returnObject = new {userRole = userLogin.role, memberId = userLogin.memberId, messageCode = messageData };
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
