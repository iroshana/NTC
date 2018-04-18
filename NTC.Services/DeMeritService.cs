using NTC.BusinessEntities;
using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.Services
{
    class DeMeritService:EntityService<DeMerit>, IDeMeritService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeMeritRepository _deMeritRepository;
        private readonly IMeritRepository _meritRepository;
        private readonly IMemberDeMeritRepository _memberDeMeritRepository;

        #region Constructor

        public DeMeritService(IUnitOfWork unitOfWork, IDeMeritRepository deMeritRepository, IMeritRepository meritRepository, IMemberDeMeritRepository memberDeMeritRepository)
            : base(unitOfWork, deMeritRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _deMeritRepository = deMeritRepository;
                _meritRepository = meritRepository;
                _memberDeMeritRepository = memberDeMeritRepository;
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

        public IList<MemberDeMeritViewModel> GetDeMeritSummery(int memberId)
        {
            try
            {
                IList<MemberDeMeritViewModel> memberDemeritView = new List<MemberDeMeritViewModel>();
                DateTime fromDate = TimeZoneInfo.ConvertTime(DateTime.Now.AddMonths(-1), TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                DateTime toDate = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["LocalTimeZone"]));
                IEnumerable<Merit> merits = _meritRepository.Get().ToList();
                foreach (Merit merit in merits)
                {
                    MemberDeMeritViewModel demerit = new MemberDeMeritViewModel();
                    demerit.code = merit.Code;
                    demerit.description = merit.Description;
                    IEnumerable<MemberDeMerit> memDeMerit = _memberDeMeritRepository.Get(x => x.MeritId == merit.ID && x.DeMerit.MemberId == memberId).ToList();
                    demerit.point = memDeMerit.Where(y=>y.DeMerit.CreatedDate.Date >= fromDate.Date && y.DeMerit.CreatedDate.Date <= toDate.Date).Sum(z=>z.Point);

                    memberDemeritView.Add(demerit);
                }
                return memberDemeritView;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

    }
}
