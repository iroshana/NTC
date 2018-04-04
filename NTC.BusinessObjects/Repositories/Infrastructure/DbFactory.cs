using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class DbFactory : Disposable, IDisposable, IDbFactory
    {
        private NTCEntities _dbContext;
        public NTCEntities Init()
        {
            return _dbContext ?? (_dbContext = new NTCEntities());
        }
        public override void DisposeCore()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }
    }
}
