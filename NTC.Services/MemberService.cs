using NTC.BusinessEntities;
using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTC.ViewModels;
using System.Data.SqlClient;

namespace NTC.Services
{
    public class MemberService:EntityService<Member>,IMemberService
    {
        #region Member Variables

        protected IUnitOfWork _unitOfWork;
        protected IMemberRepository _employeeRepository;
        protected IMemberEntityRepository _memberEntityRepository;
        protected IComplainRepository _complainRepository;
        #endregion Member Variables


        public MemberService(IUnitOfWork unitOfWork, IMemberRepository employeeRepository, IMemberEntityRepository memberEntityRepository, IComplainRepository complainRepository)
            :base(unitOfWork, employeeRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _employeeRepository = employeeRepository;
                _memberEntityRepository = memberEntityRepository;
                _complainRepository = complainRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region GetEmployee
        public Member GetMember(int Id)
        {
            try
            {
                return base.GetAll(x=>x.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        public void Add(Member member, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                IEnumerable<Member> members = base.GetAll(x=>x.NIC == member.NIC).ToList();
                if (members == null || members.Count() == 0 )
                {
                    base.Add(member);
                }
                else
                {
                    errorMessage = "Member Already in system";
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IEnumerable<Member> GetAllMembers()
        {
            try
            {
                return base.GetAll().ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<MemberEntityModel> GetAllMembersSP(int colorCode, DateTime? fromDate, DateTime? toDate, int type)
        {
            try
            {
                string errorMessage = String.Empty;
                object[] param = {
                        new SqlParameter("@colorCode", colorCode),
                        new SqlParameter("@createdDateFrom", fromDate == null ? String.Empty : fromDate.Value.ToString(@"yyyy-MM-dd")),
                        new SqlParameter("@createdDateTo", toDate == null ? String.Empty : toDate.Value.ToString(@"yyyy-MM-dd")),
                        new SqlParameter("@typeId", type)
                };
                return _memberEntityRepository.ExecuteStoredProcedure("dbo.GetMembers @colorCode, @createdDateFrom, @createdDateTo, @typeId", param).ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Member GetBestMember(DateTime date, bool isMonth)
        {
            try
            {

                IEnumerable<Member> members = base.GetAll().ToList();
                foreach (Member mem in members)
                {
                    if (mem.DeMerits == null)
                    {
                        _complainRepository.Get();
                    }
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
