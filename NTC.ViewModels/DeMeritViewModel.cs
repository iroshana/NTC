using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class DeMeritViewModel
    {
        public int id { get; set; }
        public string deMeritNo { get; set; }
        public MemberViewModel member { get; set; }
        public RouteViewModel route { get; set; }
        [Required]
        public string inqueryDate { get; set; }
        public OfficerViewModel officer { get; set; }
        public BusViewModel bus { get; set; }
        public List<MemberDeMeritViewModel> memberDeMerit { get; set; }

    }

    public class MemberDeMeritViewModel
    {
        public int id { get; set; }
        public int deMeritId { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int meritId { get; set; }
        public int point { get; set; }
        public int colorCode { get; set; }
        public bool isSelected { get; set; }
    }
    public class MeritViewModel
    {
        public int id { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int colorCodeId { get; set; }
    }

    public class OfficerViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string telNo { get; set; }
        public string nic { get; set; }
    }

    public class DeMeritTypeViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string ntcNo { get; set; }
    }

    public class DeMeritMemberTypeViewModel
    {
        public DeMeritTypeSetViewModel driver { get; set; }
        public DeMeritTypeSetViewModel conductor { get; set; }
    }

    public class DeMeritTypeSetViewModel
    {
        public List<DeMeritTypeViewModel> adPannel { get; set; }
        public List<DeMeritTypeViewModel> finePay { get; set; }
        public List<DeMeritTypeViewModel> punish { get; set; }
        public List<DeMeritTypeViewModel> cancel { get; set; }
    }

    public class ChartDeMeritViewModel
    {
        public int jan { get; set; }
        public int feb { get; set; }
        public int mar { get; set; }
        public int april { get; set; }
        public int may { get; set; }
        public int june { get; set; }
        public int july { get; set; }
        public int aug { get; set; }
        public int sep { get; set; }
        public int oct { get; set; }
        public int nov { get; set; }
        public int dec { get; set; }
    }

    public class ChartDeMeritCountViewModel
    {
        public List<int> adPannel { get; set; }
        public List<int> finePay { get; set; }
        public List<int> punish { get; set; }
        public List<int> cancel { get; set; }
    }

}
