using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class DashBoardViewModel
    {
        public string highestConductorPoints { get; set; }
        public string highestDriverPoints { get; set; }
        public int redNoticeDrivers { get; set; }
        public int redNoticeConductors { get; set; }
        public int redNoticeMembers { get; set; }
        public string highestDriverComplain { get; set; }
        public string highestConductorComplain { get; set; }
        public int bestdriversofMonth { get; set; }
        public int bestConductorsofMonth { get; set; }
        public int bestdriversofYear { get; set; }
        public int bestConductorsofYear { get; set; }
    }
}
