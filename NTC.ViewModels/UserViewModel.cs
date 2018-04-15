using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string telNo { get; set; }
        public string nic { get; set; }
        public string email { get; set; }
    }
    public class RoleViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
