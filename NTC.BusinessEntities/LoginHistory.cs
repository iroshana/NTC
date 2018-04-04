using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class LoginHistory
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public DateTime? LoggedInTime { get; set; }

        public DateTime? LoggedOutTime { get; set; }
        
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}
