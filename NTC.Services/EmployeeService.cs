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
    public class EmployeeService:EntityService<Member>,IEmployeeService
    {
        #region Member Variables

        protected IUnitOfWork _unitOfWork;
        protected IMemberRepository _employeeRepository;

        #endregion Member Variables


        public EmployeeService(IUnitOfWork unitOfWork, IMemberRepository employeeRepository)
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
        public Member GetEmployee(int Id)
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
        #endregion
    }
}
