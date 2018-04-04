using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessEntities
{
    public partial class EmployeeType
    {
        public EmployeeType()
        {
            Employees = new HashSet<Employee>();
        }
        public int ID { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
