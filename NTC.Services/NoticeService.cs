using NTC.BusinessEntities;
using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.Services
{
    public class NoticeService:EntityService<Notice>, INoticeService
    {
        #region Member Variables

        protected IUnitOfWork _unitOfWork;
        protected INoticeRepository _noticeRepository;

        #endregion Member Variables


        public NoticeService(IUnitOfWork unitOfWork, INoticeRepository noticeRepository)
            :base(unitOfWork, noticeRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _noticeRepository = noticeRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Notice Add(Notice notice, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                base.Add(notice);
                return notice;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public IEnumerable<Notice> GetAllNotices()
        {
            try
            {
                return base.GetAll(x => x.IsSent == false && x.IsGeneratNotice == true).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdaterNotice(Notice notice, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                base.Update(notice);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Notice> GetAllGeneralNotices(bool isSent,bool isGeneral)
        {
            try
            {
                return base.GetAll(x => x.IsSent == isSent && x.IsGeneratNotice == isGeneral).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
