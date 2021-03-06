﻿using NTC.BusinessEntities;
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

        public IEnumerable<MemberNotice> GetAllMemeberNotice(int memberId,bool isSent,bool isOpen)
        {
            try
            {
                return base.GetAll(x => x.MemberId == memberId && x.IsSent == isSent && x.IsOpened == isOpen).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void UpdateMemberNotice(MemberNotice notice, out string errorMessage)
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
    }
}
