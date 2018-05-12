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

        public IEnumerable<Complain> GetComplainNo(int memberId)
        {
            try
            {
                return _complainRepository.Get(x => x.DriverId == memberId || x.ConductorId == memberId).ToList();
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
                return _complainRepository.Get(x => (complainNo == String.Empty || x.ComplainNo == complainNo)
                && (x.DriverId == userId || x.ConductorId == userId)).FirstOrDefault();
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

        public IEnumerable<Complain> GetAllComplains(DateTime fromDate, DateTime toDate)
        {
            try
            {
                IEnumerable<Complain> complains = base.GetAll().ToList();
                return complains.Where(x => x.Date.Date >= fromDate && x.Date.Date <= toDate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Complain> GetAllComplains(int mrmberId)
        {
            try
            {
                return _complainRepository.Get(x=>(x.DriverId == mrmberId || x.ConductorId == mrmberId)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Update(Complain complain,List<string> properties,out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                base.Update(complain, properties, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
