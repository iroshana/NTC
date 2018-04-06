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

        public DeMerit GetDeMeritByNo(string deMeritNo)
        {
            try
            {
                return base.GetAll(x=>x.DeMeritNo == deMeritNo).FirstOrDefault();
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
        #endregion

    }
}
