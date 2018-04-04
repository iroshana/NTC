using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class ComplainCategory
    {
        [Column(Order = 0), Key, ForeignKey("Complain")]
        public int ComplainId { get; set; }
        [Column(Order = 1), Key, ForeignKey("Category")]
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public virtual Complain Complain { get; set; }
        public virtual Category Category { get; set; }
    }
}
