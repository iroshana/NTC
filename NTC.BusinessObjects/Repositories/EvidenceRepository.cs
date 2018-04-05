﻿using NTC.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTC.BusinessObjects.Repositories
{
    public class EvidenceRepository: RepositoryBase<Evidence>,IEvidenceRepository
    {
        public EvidenceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
    }
}
