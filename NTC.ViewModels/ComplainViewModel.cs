using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class ComplainViewModel
    {
        public int id { get; set; }
        public string bomplainNo { get; set; }
        public int busId { get; set; }
        public int routeId { get; set; }
        public string place { get; set; }
        public string time { get; set; }
        public string method { get; set; }
        public string complainCode { get; set; }
        public string description { get; set; }
        public int userId { get; set; }
        public int? evidenceId { get; set; }
        public int employeeId { get; set; }
        public bool isEvidenceHave { get; set; }
    }
}
