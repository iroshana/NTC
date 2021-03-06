﻿using NTC.BusinessEntities;
using NTC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.InterfaceServices
{
    public interface ICommonDataService
    {
        IEnumerable<MemberType> GetAllMemberTypes();
        IEnumerable<Bus> SearchBuses(string busNo);
        Bus GetBusByNo(string busNo);
        Bus GetBusById(int Id);
        IEnumerable<Category> GetAllCategories();
        IEnumerable<Merit> GetAllMerits();
        DashBoardEntityModel GetDashBoardCounts();
        string GetLastNTCNO(int type);
        Officer GetOfficer(string name);
    }
}
