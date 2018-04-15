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
        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IMemberEntityRepository memberEntityRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
            :base(unitOfWork, userRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _userRepository = userRepository;
                _memberEntityRepository = memberEntityRepository;
                _roleRepository = roleRepository;
                _userRoleRepository = userRoleRepository;
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

        public void registerUser(User userView, out string errorMessage)
        {
            try
            {
                errorMessage = String.Empty;
                User user = _userRepository.Get(x=>x.UserName == userView.UserName).FirstOrDefault();
                if (user!= null)
                {
                    base.Add(userView);
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

        public int validateUser(string userName, string password, out string errorMessage)
        {
            errorMessage = String.Empty;
            User user = _userRepository.Get(x=>x.UserName == userName && x.password == password).FirstOrDefault();
            UserRole userRole = new UserRole();
            if (user!= null)
            {
                userRole = _userRoleRepository.Get(x => x.UserId == user.ID).FirstOrDefault();
            }
            else
            {
                errorMessage = "Invalid User Name";
            }
            return userRole.RoleId;
        }

        public string EncryptandEncodeData(string plainText)
        {
            try
            {
                //Encrypt plain text - Start
                CspParameters cspParamsE = new CspParameters();
                cspParamsE.ProviderType = Constant.PROVIDER_RSA_FULL;

                RSACryptoServiceProvider rsaProviderE = new RSACryptoServiceProvider(cspParamsE);
                rsaProviderE.FromXmlString(Constant.PUBLIC_KEY_XML);

                var queryStringData = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedData = rsaProviderE.Encrypt(queryStringData, false);
                //Encrypt plain text - end

                //Base64 encode 
                string base64EncodedData = Convert.ToBase64String(encryptedData);

                return base64EncodedData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Decode and Decrypt Text
        public string DecodeandDecryptData(string encryptedText)
        {
            try
            {
                //base64 decode 
                byte[] base64DecodedData = Convert.FromBase64String(encryptedText);

                //Decrypt code - Start
                CspParameters cspParamsD = new CspParameters();
                cspParamsD.ProviderType = Constant.PROVIDER_RSA_FULL;

                RSACryptoServiceProvider rsaProviderD = new RSACryptoServiceProvider(cspParamsD);
                rsaProviderD.FromXmlString(Constant.PRIVATE_KEY_XML);

                byte[] decryptedData = rsaProviderD.Decrypt(base64DecodedData, false);

                string plainText = String.Empty;
                plainText = System.Text.Encoding.UTF8.GetString(decryptedData);

                return plainText;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion Decode and Decrypt Text
    }
}
