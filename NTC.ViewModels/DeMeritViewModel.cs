using System;
using System.Collections.Generic;
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
        public int routeId { get; set; }
        public string inqueryDate { get; set; }
        public OfficerViewModel officer { get; set; }
        public BusViewModel bus { get; set; }
        public List<MemberDeMeritViewModel> MemberDeMerit { get; set; }

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

}
