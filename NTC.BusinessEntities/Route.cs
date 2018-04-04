
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Route
    {
        public Route()
        {
            Complains = new HashSet<Complain>();
            Routes = new HashSet<Route>();
        }
        public int ID { get; set; }
        public String From { get; set; }
        public string To { get; set; }
        public string RouteNo { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
