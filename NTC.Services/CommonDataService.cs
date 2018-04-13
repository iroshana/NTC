using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTC.BusinessEntities;
using NTC.ViewModels;
using System.Data.SqlClient;

namespace NTC.Services
{
    public class CommonDataService : ICommonDataService
    {
        protected IMemberTypeRepository _memberTypeRepository;
        protected IBusRepository _busRepository;
        protected ICategoryRepository _categoryRepository;
        protected IMeritRepository _meritRepository;
        protected IDashBoardEntityRepository _dashBoardEntityRepository;
        protected IMemberRepository _memberRepository;
        public CommonDataService(IMemberTypeRepository memberTypeRepository, IBusRepository busRepository, ICategoryRepository categoryRepository, IMeritRepository meritRepository, IDashBoardEntityRepository dashBoardEntityRepository, IMemberRepository memberRepository)
        {
            _memberTypeRepository = memberTypeRepository;
            _busRepository = busRepository;
            _categoryRepository = categoryRepository;
            _meritRepository = meritRepository;
            _dashBoardEntityRepository = dashBoardEntityRepository;
            _memberRepository = memberRepository;
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

        public IEnumerable<Merit> GetAllMerits()
        {
            try
            {
                return _meritRepository.Get().ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Bus GetBusById(int Id)
        {
            try
            {
                return _busRepository.Get(x => x.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<DashBoardEntityModel> GetDashBoardCounts()
        {
            try
            {
                return _dashBoardEntityRepository.ExecuteStoredProcedure("dbo.DashBoard");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string GetLastNTCNO()
        {
            try
            {
                Member member = _memberRepository.Get().OrderByDescending(x => x.ID).FirstOrDefault();
                if (member != null)
                {
                    string lastNo = member.NTCNo;
                    int no = int.Parse(lastNo.Split('-')[1]);
                    return "NTC-" + (no + 1).ToString("D4");
                }
                else
                {
                    return "NTC-0001";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
