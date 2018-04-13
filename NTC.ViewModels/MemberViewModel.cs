using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTC.ViewModels
{
    public class MemberViewModel
    {
        public int id { get; set; }
        [Required]
        public string fullName { get; set; }
        public string nameWithInitial { get; set; }
        [Required]
        public string dob { get; set; }
        public string permanetAddress { get; set; }
        public string currentAddress { get; set; }
        public string telNo { get; set; }
        [Required]
        public string nic { get; set; }
        public int userID { get; set; }
        [Required]
        public int typeId { get; set; }
        public string type { get; set; }
        public string cetificateNo { get; set; }
        public string licenceNo { get; set; }
        public string trainingCenter { get; set; }
        public string educationQuali { get; set; }
        [Required]
        public string dateJoin { get; set; }
        [Required]
        public string dateIssued { get; set; }
        [Required]
        public string dateValidity { get; set; }
        public string imagePath { get; set; }
        public bool isNotification1 { get; set; }
        public bool isNotification2 { get; set; }
        public bool isNotification3 { get; set; }
        public string notification1 { get; set; }
        public string notification2 { get; set; }
        public string notification3 { get; set; }
    }
    public class MemberTypeViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
    }


    public class MemberEntityViewModel
    {
        public int id { get; set; }
        public string fullName { get; set; }
        public string trainingCenter { get; set; }
        public string trainingCertificateNo { get; set; }
        public int points { get; set; }
    }
}
