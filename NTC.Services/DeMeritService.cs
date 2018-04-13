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
    class DeMeritService:EntityService<DeMerit>, IDeMeritService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeMeritRepository _deMeritRepository;
        

        #region Constructor

        public DeMeritService(IUnitOfWork unitOfWork, IDeMeritRepository deMeritRepository)
            : base(unitOfWork, deMeritRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _deMeritRepository = deMeritRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Add(DeMerit deMerit, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                base.Add(deMerit);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DeMerit GetDeMeritByNo(int deMeritId)
        {
            try
            {
                return base.GetAll(x=>x.ID == deMeritId).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<DeMerit> GetDeMeritByUser(int memberId)
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

        public DeMerit GetLastDeMeritNo()
        {
            try
            {
                return base.GetAll().OrderBy(x => x.ID).LastOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<DeMerit> GetAllDeMerits()
        {
            try
            {
                IEnumerable<DeMerit> demerits = _deMeritRepository.Get();
                return demerits.Where(x=>x.CreatedDate.Date <= DateTime.Now.Date && x.CreatedDate.Date > DateTime.Now.Date.AddMonths(-1));
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
