using NTC.BusinessEntities;
using NTC.BusinessObjects.Repositories;
using NTC.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NTC.ViewModels;

namespace NTC.Services
{
    public class UserService:EntityService<User>, IUserService
    {
        protected IUnitOfWork _unitOfWork;
        protected IUserRepository _userRepository;
        protected IRoleRepository _roleRepository;
        protected IUserRoleRepository _userRoleRepository;
        protected IMemberEntityRepository _memberEntityRepository;
        protected IMemberRepository _memberRepository;
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMemberEntityRepository memberEntityRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IMemberRepository memberRepository)
            :base(unitOfWork, userRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _userRepository = userRepository;
                _memberEntityRepository = memberEntityRepository;
                _roleRepository = roleRepository;
                _userRoleRepository = userRoleRepository;
                _memberRepository = memberRepository;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Role> GetUserRoles()
        {
            try
            {
                return _roleRepository.Get().ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void registerUser(User userView, int roleId, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                User user = _userRepository.Get(x=>x.UserName == userView.UserName).FirstOrDefault();
                if (user == null)
                {
                    base.Add(userView);
                    //if (roleId != 0)
                    //{
                        
                    //}
                    if (userView.UserName.StartsWith("D") || userView.UserName.StartsWith("C"))
                    {
                        List<string> properties = new List<string>();
                        properties.Add("UserID");

                        Member member = new Member();
                        member = _memberRepository.Get(x=>x.NTCNo == userView.UserName).FirstOrDefault();
                        member.UserID = userView.ID;
                        _memberRepository.Update(member, properties, true);
                    }
                }
                else
                {
                    errorMessage = "UserName Already exists";
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public UserLoginViewModel validateUser(string userName, string password, out string errorMessage)
        {
            errorMessage = String.Empty;
            User user = _userRepository.Get(x=>x.UserName == userName && x.password == password).FirstOrDefault();
            UserRole userRole = new UserRole();
            if (user!= null)
            {
                userRole = _userRoleRepository.Get(x => x.UserId == user.ID).FirstOrDefault();
                UserLoginViewModel userLogin = new UserLoginViewModel();

                var memberId = _memberRepository.Get(x => x.NTCNo == userName).FirstOrDefault();
                if(memberId != null)
                {
                    userLogin.memberId = memberId.ID;
                }
                else
                {
                    userLogin.memberId = 0;
                }
                
                userLogin.role = userRole.Role.Code;
                return userLogin;
            }
            else
            {
                errorMessage = "Invalid User Name or password";
                UserLoginViewModel userLogin = new UserLoginViewModel();
                return userLogin;
            }
           
        }
        
    }
}
