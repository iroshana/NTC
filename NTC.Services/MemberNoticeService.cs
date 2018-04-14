using NTC.BusinessEntities;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTC.BusinessObjects.Repositories;

namespace NTC.Services
{
    public class MemberNoticeService : EntityService<MemberNotice>, IMemberNoticeService
    {
        protected IUnitOfWork _unitOfWork;
        protected IMemberNoticeRepository _memberNotice;

        public MemberNoticeService(IUnitOfWork unitOfWork, IMemberNoticeRepository memberNotice)
            :base(unitOfWork, memberNotice)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _memberNotice = memberNotice;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<MemberNotice> GetAllMemeberNotice(int memberId)
        {
            try
            {
                return base.GetAll(x => x.MemberId == memberId).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
