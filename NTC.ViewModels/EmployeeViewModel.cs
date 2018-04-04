using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class EmployeeViewModel
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string dob { get; set; }
        public string privateAddress { get; set; }
        public string currentAddress { get; set; }
        public string telNo { get; set; }
        public string nic { get; set; }
        public int userID { get; set; }
        public int typeId { get; set; }
        public string trainingCertificateNo { get; set; }
        public string licenceNo { get; set; }
        public string trainingCenter { get; set; }
        public string highestEducation { get; set; }
        public string joinDate { get; set; }
        public string issuedDate { get; set; }
        public string expireDate { get; set; }
        public string imagePath { get; set; }
        public bool isNotification1 { get; set; }
        public bool isNotification2 { get; set; }
        public bool isNotification3 { get; set; }
        public string notification1 { get; set; }
        public string notification2 { get; set; }
        public string notification3 { get; set; }
    }
}
