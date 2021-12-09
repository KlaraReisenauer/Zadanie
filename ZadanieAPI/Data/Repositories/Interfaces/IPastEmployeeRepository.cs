﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZadanieAPI.Data.DTOs;

namespace ZadanieAPI.Data.Repositories.Interfaces
{
    public interface IPastEmployeeRepository
    {
        public IList<PastEmployee> GetAll();

        public PastEmployee GetById(Guid id);

        public bool Remove(Guid id);

    }
}
