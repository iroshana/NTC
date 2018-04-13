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

}
