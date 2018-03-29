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
        private NTCModel _dbContext;
        public NTCModel Init()
        {
            return _dbContext ?? (_dbContext = new NTCModel());
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
