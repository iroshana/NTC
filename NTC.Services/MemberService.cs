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
    public class MemberService:EntityService<Member>,IMemberService
    {
        #region Member Variables

        protected IUnitOfWork _unitOfWork;
        protected IMemberRepository _employeeRepository;

        #endregion Member Variables


        public MemberService(IUnitOfWork unitOfWork, IMemberRepository employeeRepository)
            :base(unitOfWork, employeeRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _employeeRepository = employeeRepository;
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
                return base.GetAll(x=>x.User.ID == Id).FirstOrDefault();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

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
        #endregion
    }
}
