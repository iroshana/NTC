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
        public int officeriId { get; set; }
        public int busId { get; set; }
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
    }
    public class Merit
    {
        public int iD { get; set; }
        public string code { get; set; }
        public string description { get; set; }
        public int colorCodeId { get; set; }
    }

}
