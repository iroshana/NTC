using NTC.BusinessEntities;
using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.Services
{
    public class EventLogService : IEventLogService
    {
        #region Member Variables

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogmessageRepository _logMessageRepository;

        #endregion Member Variables

        #region Constructor

        public EventLogService(IUnitOfWork unitOfWork, ILogmessageRepository logMessageRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _logMessageRepository = logMessageRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Constructor

        #region Public Methods

        public string WriteLogs(string userName, Exception exception, string applicationName)
        {
            LogMessage errorLog = new LogMessage();
            errorLog.ApplicationName = applicationName;
            errorLog.Message = String.Format("{0} {1}", exception.Message, exception.StackTrace);
            errorLog.User = "Arjuna";
            errorLog.CreatedDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));

            _logMessageRepository.Add(errorLog);
            _unitOfWork.Commit();
            return errorLog.ID.ToString();
        }

        #endregion Public Methods
    }
}
