﻿using Shrimply.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shrimply.DataAccess.Repository.IRepository
{
    public interface IShrimpRepository : IRepository<Shrimp>
    {
        void Update(Shrimp shrimp);
    }
}
