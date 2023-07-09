﻿using MicroserviceTest.Contract.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Core.Data
{
    public interface IUserRepository
    {
        Task CreateAsync(UserData userData);
        Task<UserData> ReadAsync(string id);
        Task UpdateAsync(UserData userData);
        Task DeleteAsync(string id);
    }
}
