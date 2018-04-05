using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTC.BusinessEntities;

namespace NTC.Services
{
    public class CommonDataService : ICommonDataService
    {
        protected IMemberTypeRepository _memberTypeRepository;
        protected IBusRepository _busRepository;
        protected ICategoryRepository _categoryRepository;
        public CommonDataService(IMemberTypeRepository memberTypeRepository, IBusRepository busRepository, ICategoryRepository categoryRepository)
        {
            _memberTypeRepository = memberTypeRepository;
            _busRepository = busRepository;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<MemberType> GetAllMemberTypes()
        {
            try
            {
                return _memberTypeRepository.Get().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Bus GetBusByNo(string busNo)
        {
            try
            {
                return _busRepository.Get(x=>x.LicenceNo == busNo).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Bus> SearchBuses(string busNo)
        {
            try
            {
                return _busRepository.Get(x=>x.LicenceNo.StartsWith(busNo)).ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            try
            {
                return _categoryRepository.Get().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
