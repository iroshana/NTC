using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class User
    {
        public User()
        {
            Complains = new HashSet<Complain>();
            LoginHistories = new HashSet<LoginHistory>();
        }
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string TelNo { get; set; }
        public string NIC { get; set; }
        public string Email { get; set; }
        public DateTime LastLogin { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
    }
}
