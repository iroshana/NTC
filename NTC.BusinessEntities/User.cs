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
            DeMerits = new HashSet<DeMerit>();
            LoginHistories = new HashSet<LoginHistory>();
        }
        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        [StringLength(1000)]
        public string PrivateAddress { get; set; }
        [StringLength(1000)]
        public string CUrrentAddress { get; set; }
        public string TelNo { get; set; }
        public string NIC { get; set; }
        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<DeMerit> DeMerits { get; set; }
        public virtual ICollection<LoginHistory> LoginHistories { get; set; }
    }
}
