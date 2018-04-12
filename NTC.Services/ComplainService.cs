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
        protected IEvidenceRepository _evideneceRepository;

        #endregion Member Variables


        public ComplainService(IUnitOfWork unitOfWork, IComplainRepository complainRepository, IEvidenceRepository evideneceRepository)
            :base(unitOfWork, complainRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _complainRepository = complainRepository;
                _evideneceRepository = evideneceRepository;
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
                return _complainRepository.Get(x => x.MemberId == userId).ToList();
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
                return _complainRepository.Get(x => x.ComplainNo == complainNo || x.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Add(Complain complain, out string errorMessage)
        {
            try
            {
                using (var dbContextTransaction = _complainRepository.DbContext.Database.BeginTransaction())
                {
                    try
                    {
                        errorMessage = String.Empty;
                        base.Add(complain);
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
            
        }

        public Complain GetLastComplain()
        {
            try
            {
                return base.GetAll().OrderBy(x=>x.ID).LastOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
