using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IEventLogService
    {
        #region Methods
        string WriteLogs(string userName, Exception exception, string applicationName);
        #endregion
    }
}
