using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class Disposable : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false;

        ~Disposable()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!disposedValue && disposing)
            {
                DisposeCore();
            }
            disposedValue = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void DisposeCore()
        {

        }
        #endregion
    }
}
