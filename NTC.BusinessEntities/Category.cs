using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class Category
    {
        public Category()
        {
            ComplainCategories = new HashSet<ComplainCategory>();
        }
        public int ID { get; set; }
        public string CategoryNo { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ComplainCategory> ComplainCategories { get; set; }

    }
}
