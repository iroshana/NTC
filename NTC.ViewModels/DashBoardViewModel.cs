using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.ViewModels
{
    public class DashBoardViewModel
    {
        public int highestConductorPoints { get; set; }
        public int highestDriverPoints { get; set; }
        public int redNoticeDrivers { get; set; }
        public int redNoticeConductors { get; set; }
        public int redNoticeMembers { get; set; }
        public int highestDriverComplain { get; set; }
        public int highestConductorComplain { get; set; }
    }
}
