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

        public void Add(Notice notice, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                base.Add(notice);
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
                return base.GetAll(x=>x.IsSent == false).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}
