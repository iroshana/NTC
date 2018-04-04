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
    public class ComplainService:EntityService<Complain>, IComplainService
    {
        #region Member Variables

        protected IUnitOfWork _unitOfWork;
        protected IComplainRepository _complainRepository;

        #endregion Member Variables


        public ComplainService(IUnitOfWork unitOfWork, IComplainRepository complainRepository)
            :base(unitOfWork, complainRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _complainRepository = complainRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Complain> GetComplainNo(int userId)
        {
            try
            {
                return _complainRepository.Get(x => x.UserId == userId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Complain GetComplainByNo(string complainNo, int userId)
        {
            try
            {
                return _complainRepository.Get(x => x.ComplainNo == complainNo && x.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
