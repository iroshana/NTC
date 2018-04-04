using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface IEmployeeService:IEntityService<Employee>
    {
        Employee GetEmployee(int Id);
    }
}
