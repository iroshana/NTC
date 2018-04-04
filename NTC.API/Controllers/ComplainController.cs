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
        public IHttpActionResult GetComplainByNo(string complainNo)
        {
            try
            {
                ComplainViewModel complainView = new ComplainViewModel();
                Complain complain = new Complain();
                complain = _complain.GetComplainByNo(complainNo);
                if (complain != null)
                {
                    
                }


                var messageData = new { code = "Test", message = "Goda" };
                var returnObject = new { item = complainView, messageCode = messageData };
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
