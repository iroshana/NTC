using NTC.BusinessEntities;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IUserService:IEntityService<User>
    {
        IEnumerable<Role> GetUserRoles();
        void registerUser(User user, int roleId, out string errorMessage);
        string validateUser(string userName,string password, out string errorMessage);
        //string DecodeandDecryptData(string encryptedText);
        //string EncryptandEncodeData(string plainText);
    }
}
